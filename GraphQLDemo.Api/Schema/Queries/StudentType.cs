namespace GraphQLDemo.Api.Schema.Queries;

public class StudentType
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [GraphQLName("gpa")]
    public double GPA { get; set; }
}