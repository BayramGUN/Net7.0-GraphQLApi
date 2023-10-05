using GraphQLDemo.Api.Models;

namespace GraphQLDemo.Api.Schema.Mutations.Course;

public record CourseResult
(
    Guid Id,
    Guid InstructorId,
    string Title,
    Subject Subject
);