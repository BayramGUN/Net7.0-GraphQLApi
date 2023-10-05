using System.IO.Compression;
using Bogus;
using GraphQLDemo.Api.DTOs;
using GraphQLDemo.Api.Models;
using GraphQLDemo.Api.Services.Courses;
using GraphQLDemo.Api.Services.Instructors;
using GraphQLDemo.Api.Services.Students;
using Microsoft.EntityFrameworkCore.Internal;

namespace GraphQLDemo.Api.Schema.Queries;

public class Query
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IInstructorsRepository _instructorRepository;
    private readonly IStudentsRepository _studentRepository;

    public Query(ICoursesRepository coursesRepository,
                 IInstructorsRepository instructorRepository,
                 IStudentsRepository studentRepository)
    {
        _coursesRepository = coursesRepository;
        _instructorRepository = instructorRepository;
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<InstructorType>> GetInstructorsAsync() 
    {
        IEnumerable<InstructorDTO> instructorDTOs = await _instructorRepository.GetAllAsync();
        return instructorDTOs.Select(i => new InstructorType
        {
            Id = i.Id,
            FullName = $"{i.FirstName} {i.LastName}",
            Salary = i.Salary
        });
    }
    public async Task<IEnumerable<CourseType>> GetCoursesAsync()
    {
        IEnumerable<CourseDTO> courses = await _coursesRepository.GetAllAsync();
        return courses.Select(c => new CourseType
        {
            Id = c.Id,
            Title = c.Title,
            Subject = c.Subject,
            InstructorId = c.InstructorId,
            Students = c.Students?.SelectMany(s => new List<StudentType>
            {
                new()
                {
                    Id = s.Id,
                    GPA = s.GPA,
                    FullName = $"{s.FirstName} {s.LastName}",

                }
            }).ToList(),
            
        }).ToList();
    }
    public async Task<IEnumerable<StudentType>> GetStudentsAsync()
    {
        IEnumerable<StudentDTO> students = await _studentRepository.GetAllAsync();
        return students.Select(s => new StudentType
        {
            Id = s.Id,
            FullName = $"{s.FirstName} {s.LastName}",
            GPA = s.GPA,
            Courses = s.Courses?.SelectMany(s => new List<CourseType>
            {
                new()
                {
                    Id = s.Id,
                    Title = s.Title,
                    Subject = s.Subject,
                    InstructorId = s.InstructorId
                }
            }).ToList(),
            
        }).ToList();
    }

    

    public async Task<CourseType> GetCourseByIdAsync(Guid id)
    {
        var course = await _coursesRepository.GetByIdAsync(id);
        return new CourseType()
        {
            Id = course.Id,
            Title = course.Title,
            Subject = course.Subject,
            InstructorId = course.InstructorId,
            Students = course.Students?.SelectMany(s => new List<StudentType>
            {
                new()
                {
                    Id = s.Id,
                    GPA = s.GPA,
                    FullName = $"{s.FirstName} {s.LastName}",
                }
            }).ToList()
        };
    }

    [GraphQLDeprecated("This course query is deprecated.")]
    public string Instructions => "Smash that like button and subscribe to Us!";
}