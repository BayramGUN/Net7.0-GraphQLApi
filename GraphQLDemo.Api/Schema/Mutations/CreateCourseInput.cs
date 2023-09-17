using GraphQLDemo.Api.Models;
using GraphQLDemo.Api.Schema.Queries;

namespace GraphQLDemo.Api.Schema.Mutations;

public record CreateCourseInput(string Title, Subject Subject, Guid InstructorId);
