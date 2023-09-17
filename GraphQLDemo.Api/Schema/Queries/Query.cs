using System.IO.Compression;
using Bogus;
using GraphQLDemo.Api.DTOs;
using GraphQLDemo.Api.Models;
using GraphQLDemo.Api.Services.Courses;
using Microsoft.EntityFrameworkCore.Internal;

namespace GraphQLDemo.Api.Schema.Queries;

public class Query
{
    private readonly ICoursesRepository _coursesRepository;

    public Query(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    
    public async Task<IEnumerable<CourseType>> GetCoursesAsync()
    {
        IEnumerable<CourseDTO> courses = await _coursesRepository.GetAllAsync();
        return courses.Select(c => new CourseType
        {
            Id = c.Id,
            Title = c.Title,
            Subject = c.Subject,
            Students = c.Students?.SelectMany(s => new List<StudentType>
            {
                new()
                {
                    Id = s.Id,
                    GPA = s.GPA,
                    FullName = $"{s.FirstName} {s.LastName}",

                }
            }).ToList(),
            Instructor = new InstructorType()
            {
                Id = c.Instructor!.Id,
                FullName = $"{c.Instructor.FirstName} {c.Instructor.LastName}",
                Salary = c.Instructor.Salary
            }
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
            Students = course.Students?.SelectMany(s => new List<StudentType>
            {
                new()
                {
                    Id = s.Id,
                    GPA = s.GPA,
                    FullName = $"{s.FirstName} {s.LastName}",
                }
            }).ToList(),
            Instructor = new InstructorType()
            {
                Id = course.Instructor!.Id,
                FullName = $"{course.Instructor.FirstName} {course.Instructor.LastName}",
                Salary = course.Instructor.Salary
            }
        };
    }

    [GraphQLDeprecated("This query is deprecated.")]
    public string Instructions => "Smash that like button and subscribe to Us!";
}