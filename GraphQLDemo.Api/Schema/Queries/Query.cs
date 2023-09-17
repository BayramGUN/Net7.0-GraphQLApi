using System.IO.Compression;
using Bogus;

namespace GraphQLDemo.Api.Schema.Queries;

public class Query
{
    private readonly Faker<CourseType> _courseFaker = null!;
    private readonly Faker<StudentType> _studentFaker = null!;
    private readonly Faker<InstructorType> _instructorFaker = null!;
    public Query()
    {
        _instructorFaker = new Faker<InstructorType>()
            .RuleFor(i => i.Id, f => Guid.NewGuid())
            .RuleFor(i => i.FirstName, f => f.Name.FirstName())
            .RuleFor(i => i.LastName, f => f.Name.LastName())
            .RuleFor(i => i.Salary, f => Math.Round(f.Random.Double(3000, 10000), 2));

        _studentFaker = new Faker<StudentType>()
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.FirstName, f => f.Name.FirstName())
            .RuleFor(s => s.LastName, f => f.Name.LastName())
            .RuleFor(s => s.GPA, f => Math.Round(f.Random.Double(0, 4), 2));

        _courseFaker = new Faker<CourseType>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Title, f => f.Name.JobTitle())
            .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
            .RuleFor(c => c.Instructor, f => _instructorFaker.Generate())
            .RuleFor(c => c.Students, f => _studentFaker.Generate(3));
    }
    public IEnumerable<StudentType> GetStudents() =>
        _studentFaker.Generate(2);
    public IEnumerable<InstructorType> GetInstructors() =>
        _instructorFaker.Generate(2);
    public IEnumerable<CourseType> GetCourses() =>
        _courseFaker.Generate(5);

    public async Task<CourseType> GetCourseByIdAsync(Guid id)
    {
        await Task.Delay(1000);
        var course = _courseFaker.Generate();
        course.Id = id;
        return course;
    }

    /* new List<CourseType>()
{
   new CourseType()
   {
       Id = Guid.NewGuid(),
       Title = "Geometry",
       Subject = Subject.Mathematics,
       Instructor = new InstructorType()
       {
           Id = 
       }
   }
} */

    [GraphQLDeprecated("This query is deprecated.")]
    public string Instructions => "Smash that like button and subscribe to Us!";
}