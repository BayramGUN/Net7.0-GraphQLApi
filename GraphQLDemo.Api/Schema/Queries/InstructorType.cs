namespace GraphQLDemo.Api.Schema.Queries;

public class InstructorType
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public double Salary { get; set; }
}