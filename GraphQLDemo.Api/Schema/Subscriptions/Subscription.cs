using GraphQLDemo.Api.Schema.Mutations;
using GraphQLDemo.Api.Schema.Queries;

namespace GraphQLDemo.Api.Schema.Subscriptions;

public class Subscription
{
    [Subscribe]
    public CourseResult CourseCreated([EventMessage] CourseResult course) => course;
    [Subscribe]
    public CourseResult CourseUpdated([EventMessage] CourseResult course) => course;
} 