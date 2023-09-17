using Bogus;
using GraphQLDemo.Api.DTOs;
using GraphQLDemo.Api.Schema.Queries;
using GraphQLDemo.Api.Schema.Subscriptions;
using GraphQLDemo.Api.Services.Courses;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.Api.Schema.Mutations;

public class Mutation
{
    private readonly ICoursesRepository _coursesRepository;

    public Mutation(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }


    public async Task<CourseResult> CreateCourseAsync(CreateCourseInput createCourse,
                                     [Service] ITopicEventSender topicEventSender)
    {

        CourseDTO courseDTO = new CourseDTO()
        {
            Title = createCourse.Title,
            Subject = createCourse.Subject,
            InstructorId = createCourse.InstructorId
        };

        courseDTO = await _coursesRepository!.CreateAsync(courseDTO);
        
        CourseResult courseResult = new ()
        {
            Id = courseDTO.Id,
            Title = courseDTO.Title,
            Subject = courseDTO.Subject,
            InstructorId = courseDTO.InstructorId
        };
        
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseResult);
        return courseResult;
    }
    public async Task<CourseResult> UpdateCourseAsync(UpdateCourseInput updateCourse, [Service] ITopicEventSender topicEventSender)
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

        CourseResult courseResult = new ()
        {
            Id = courseDTO.Id,
            Title = courseDTO.Title,
            Subject = courseDTO.Subject,
            InstructorId = courseDTO.InstructorId
        };
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
}