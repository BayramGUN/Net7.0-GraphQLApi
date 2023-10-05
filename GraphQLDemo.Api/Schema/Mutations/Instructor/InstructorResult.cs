using GraphQLDemo.Api.Models;

namespace GraphQLDemo.Api.Schema.Mutations.Instructor;

public record InstructorResult
(
    Guid Id,
    double Salary,
    string FullName
);