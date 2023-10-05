using GraphQLDemo.Api.Models;

namespace GraphQLDemo.Api.Schema.Mutations.Instructor;

public record CreateInstructorInput(
    string FirstName,
    string LastName,
    double Salary
);
