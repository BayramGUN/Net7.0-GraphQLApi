using GraphQLDemo.Api.Models;

namespace GraphQLDemo.Api.DTOs;

public class CourseDTO
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public Subject Subject { get; set; }
    public required Guid InstructorId { get; set; }
    public InstructorDTO? Instructor { get; set; }
    public IEnumerable<StudentDTO>? Students { get; set; }
}