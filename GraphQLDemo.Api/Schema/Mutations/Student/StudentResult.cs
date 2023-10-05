
using GraphQLDemo.Api.Schema.Mutations.Course;

namespace GraphQLDemo.Api.Schema.Mutations.Student;

public record StudentResult
(
    Guid Id,
    double GPA,
    string FullName
);