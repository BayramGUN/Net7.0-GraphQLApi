
namespace GraphQLDemo.Api.Schema.Mutations.Student;

public record CreateStudentInput(
    string FirstName,
    string LastName,
    double GPA
);
