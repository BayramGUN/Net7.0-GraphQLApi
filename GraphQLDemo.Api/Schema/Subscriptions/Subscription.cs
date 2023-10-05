using GraphQLDemo.Api.Schema.Mutations.Course;
using GraphQLDemo.Api.Schema.Mutations.Instructor;

namespace GraphQLDemo.Api.Schema.Subscriptions;

public class Subscription
{
    [Subscribe]
    public CourseResult CourseCreated([EventMessage] CourseResult course) 
        => course;
    [Subscribe]
    public CourseResult CourseUpdated([EventMessage] CourseResult course) 
        => course;
    [Subscribe]
    public InstructorResult InstructorCreated([EventMessage] InstructorResult instructor) 
        => instructor;
} 