using GraphQLDemo.Api.Schema.Queries;

namespace GraphQLDemo.Api.Schema.Mutations;

public record UpdateCourseInput(Guid Id, string Title, Subject Subject, Guid InstructorId);
