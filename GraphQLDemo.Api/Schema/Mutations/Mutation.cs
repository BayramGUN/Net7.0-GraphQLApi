using System.Runtime.CompilerServices;
using Bogus;
using GraphQLDemo.Api.DTOs;
using GraphQLDemo.Api.Schema.Mutations.Course;
using GraphQLDemo.Api.Schema.Mutations.Instructor;
using GraphQLDemo.Api.Schema.Mutations.Student;
using GraphQLDemo.Api.Schema.Queries;
using GraphQLDemo.Api.Schema.Subscriptions;
using GraphQLDemo.Api.Services.Courses;
using GraphQLDemo.Api.Services.Instructors;
using GraphQLDemo.Api.Services.Students;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace GraphQLDemo.Api.Schema.Mutations;

public class Mutation
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IInstructorsRepository _instructorsRepository;
    private readonly IStudentsRepository _studentRepository;

    public Mutation(
        ICoursesRepository coursesRepository,
        IInstructorsRepository instructorsRepository,
        IStudentsRepository studentRepository)
    {
        _coursesRepository = coursesRepository;
        _instructorsRepository = instructorsRepository;
        _studentRepository = studentRepository;
    }


    public async Task<CourseResult> CreateCourseAsync(
        CreateCourseInput createCourse,
        [Service] ITopicEventSender topicEventSender)
    {

        CourseDTO courseDTO = new CourseDTO()
        {
            Title = createCourse.Title,
            Subject = createCourse.Subject,
            InstructorId = createCourse.InstructorId
        };

        courseDTO = await _coursesRepository!.CreateAsync(courseDTO);
        
        var courseResult = new CourseResult(
            courseDTO.Id,
            courseDTO.InstructorId,
            courseDTO.Title,
            courseDTO.Subject
        );
        
        
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseResult);
        return courseResult;
    }
    public async Task<StudentResult> CreateStudentAsync(
        CreateStudentInput createStudent,
        [Service] ITopicEventSender topicEventSender)
    {

        StudentDTO studentDTO = new()
        {
            FirstName = createStudent.FirstName,
            LastName = createStudent.LastName,
            GPA = createStudent.GPA
        };

        studentDTO = await _studentRepository!.CreateAsync(studentDTO);
        
        var studentResult = new StudentResult(
            studentDTO.Id,
            studentDTO.GPA,
            $"{studentDTO.FirstName} {studentDTO.LastName}"
        );
        
        
        //await topicEventSender.SendAsync(nameof(Subscription.studentCreated), StudentResult);
        return studentResult;
    }
    public async Task<InstructorResult> CreateInstructorAsync(
        CreateInstructorInput createInstructor,
        [Service] ITopicEventSender topicEventSender)
    {

        InstructorDTO instructorDTO = new InstructorDTO()
        {
            FirstName = createInstructor.FirstName,
            LastName = createInstructor.LastName,
            Salary = createInstructor.Salary
        };

        instructorDTO = await _instructorsRepository!.CreateAsync(instructorDTO);
        
        var instructorResult = new InstructorResult(
            instructorDTO.Id,
            instructorDTO.Salary,
            $"{instructorDTO.FirstName} {instructorDTO.FirstName}"
        );
        
        await topicEventSender.SendAsync(nameof(Subscription.InstructorCreated), instructorResult);
        return instructorResult;
    }
    public async Task<CourseResult> UpdateCourseAsync(
        UpdateCourseInput updateCourse,
        [Service] ITopicEventSender topicEventSender)
    {
        if(!await _coursesRepository.IsExistAsync(updateCourse.Id))
            throw new GraphQLException(new Error(
                $"There is no course found with given id: {updateCourse.Id}!",
                "COURSE_NOT_FOUND"));
        CourseDTO courseDTO = new CourseDTO()
        {
            Id = updateCourse.Id,
            Title = updateCourse.Title,
            Subject = updateCourse.Subject,
            InstructorId = updateCourse.InstructorId
        };
        await _coursesRepository.UpdateAsync(courseDTO);

        var courseResult = new CourseResult(
            courseDTO.Id,
            courseDTO.InstructorId,
            courseDTO.Title,
            courseDTO.Subject
        );
        await topicEventSender.SendAsync($"{ courseResult.Id}{nameof(Subscription.CourseUpdated)}", courseResult);
        
        return courseResult;
    }
    public async Task<bool> DeleteCourseAsync(Guid id)
    {
        if(!await _coursesRepository.IsExistAsync(id))
            throw new GraphQLException(new Error(
                $"There is no course found with given id: {id}!",
                "COURSE_NOT_FOUND"));
        return await _coursesRepository.DeleteAsync(id);
    } 
    public async Task<CourseWithStudentsResult> RegisterStudentsToCourse(List<Guid> studentIds, Guid courseId)
    {
        if(!await _coursesRepository.IsExistAsync(courseId))
            throw new GraphQLException(new Error(
                $"There is no course found with given id: {courseId}!",
                "COURSE_NOT_FOUND"));
        var students = await _studentRepository.GetManyByIdsAsync(studentIds);
        
        var courseWithNewStudents = await _coursesRepository.AddStudentsToCourseAsync(students, courseId);
        return new CourseWithStudentsResult(
            courseWithNewStudents.Id,
            courseWithNewStudents.InstructorId,
            courseWithNewStudents.Title,
            courseWithNewStudents.Subject,
            courseWithNewStudents.Students?.Select(s => new StudentType
            {
                Id = s.Id,
                FullName = $"{s.FirstName} {s.LastName}",
                GPA = s.GPA
            }).ToList()!
        );
    }
}