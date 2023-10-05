using GraphQLDemo.Api.Models;

namespace GraphQLDemo.Api.Schema.Mutations.Course;

public record UpdateCourseInput(Guid Id, string Title, Subject Subject, Guid InstructorId);
