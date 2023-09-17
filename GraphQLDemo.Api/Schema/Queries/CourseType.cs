using GraphQLDemo.Api.Models;

namespace GraphQLDemo.Api.Schema.Queries;

public class CourseType
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public Subject Subject { get; set; }
    [GraphQLNonNullType]
    public required InstructorType Instructor { get; set; }
    public IEnumerable<StudentType>? Students { get; set; }
}