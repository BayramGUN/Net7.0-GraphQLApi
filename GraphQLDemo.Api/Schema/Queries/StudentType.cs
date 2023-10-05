namespace GraphQLDemo.Api.Schema.Queries;

public class StudentType
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    [GraphQLName("gpa")]
    public double GPA { get; set; }
    public IEnumerable<CourseType>? Courses { get; set; }
}