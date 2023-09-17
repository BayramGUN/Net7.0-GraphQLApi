namespace GraphQLDemo.Api.DTOs;

public class InstructorDTO
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public double Salary { get; set; }
}