using GraphQLDemo.Api.Schema.Queries;

namespace GraphQLDemo.Api.Schema.Mutations;

public class CourseResult
{
    public Guid Id { get; set; }
    public Guid InstructorId { get; set; }
    public required string Title { get; set; }
    public Subject Subject { get; set; }
}