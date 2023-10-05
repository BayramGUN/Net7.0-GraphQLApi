using GraphQLDemo.Api.Models;
using GraphQLDemo.Api.Schema.Mutations.Student;
using GraphQLDemo.Api.Schema.Queries;

namespace GraphQLDemo.Api.Schema.Mutations.Course;

public record CourseWithStudentsResult
(
    Guid Id,
    Guid InstructorId,
    string Title,
    Subject Subject,
    IEnumerable<StudentType> Students
);