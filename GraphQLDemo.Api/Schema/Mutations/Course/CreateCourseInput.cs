using GraphQLDemo.Api.Models;

namespace GraphQLDemo.Api.Schema.Mutations.Course;

public record CreateCourseInput(string Title, Subject Subject, Guid InstructorId);
