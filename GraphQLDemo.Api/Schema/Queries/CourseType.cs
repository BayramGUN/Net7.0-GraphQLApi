using System.Runtime.CompilerServices;
using GraphQLDemo.Api.DataLoaders;
using GraphQLDemo.Api.Models;
using GraphQLDemo.Api.Services.Instructors;

namespace GraphQLDemo.Api.Schema.Queries;

public class CourseType
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public Subject Subject { get; set; }
    [GraphQLIgnore]
    public Guid InstructorId { get; set; }
    [GraphQLNonNullType]
    public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
    {
        var instructorDto = await instructorDataLoader.LoadAsync(InstructorId, CancellationToken.None);

        return new InstructorType()
        {
            Id = instructorDto.Id,
            FullName = $"{instructorDto.FirstName} {instructorDto.LastName}",
            Salary = instructorDto.Salary
        };
    }

    public IEnumerable<StudentType>? Students { get; set; }
}