namespace GraphQLDemo.Api.Schema.Queries;

public class InstructorType
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public double Salary { get; set; }
}