using Bogus;
using GraphQLDemo.Api.Schema.Queries;
using GraphQLDemo.Api.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.Api.Schema.Mutations;

public class Mutation
{
    private readonly List<CourseResult> _courses;

    public Mutation()
    {
        _courses = new();  
    }

    public async Task<CourseResult> CreateCourseAsync(CreateCourseInput createCourse,
                                     [Service] ITopicEventSender topicEventSender)
    {
        
        CourseResult courseResult = new ()
        {
            Id = Guid.NewGuid(),
            Title = createCourse.Title,
            Subject = createCourse.Subject,
            InstructorId = createCourse.InstructorId
        };
        
        _courses?.Add(courseResult);
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseResult);
        return courseResult;
    }
    public async Task<CourseResult> UpdateCourseAsync(UpdateCourseInput updateCourse, [Service] ITopicEventSender topicEventSender)
    {
        var willUpdateCourse = 
            _courses!.FirstOrDefault(c => c.Id == updateCourse.Id) 
            ?? throw new GraphQLException(new Error(
                $"There is no course found with that id: {updateCourse.Id}!",
                "COURSE_NOT_FOUND"));

        willUpdateCourse.Title = updateCourse.Title;
        willUpdateCourse.Subject = updateCourse.Subject;
        willUpdateCourse.InstructorId = updateCourse.InstructorId;
        await topicEventSender.SendAsync($"{ willUpdateCourse.Id}{nameof(Subscription.CourseUpdated)}", willUpdateCourse);
        
        return willUpdateCourse;
    }
    public bool DeleteCourse(Guid id)
    {
        return _courses.RemoveAll(c => c.Id == id) == 1;
    } 
}