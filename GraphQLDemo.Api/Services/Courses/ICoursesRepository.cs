using GraphQLDemo.Api.DTOs;

namespace GraphQLDemo.Api.Services.Courses;

public interface ICoursesRepository
{
    // Mutations/Commands
    Task<CourseDTO> CreateAsync(CourseDTO course);
    Task<CourseDTO> UpdateAsync(CourseDTO course);
    Task<CourseDTO> AddStudentsToCourseAsync(IEnumerable<StudentDTO> students, Guid courseId);
    Task<bool> DeleteAsync(Guid id);

    // Queries
    Task<bool> IsExistAsync(Guid id);
    Task<CourseDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<CourseDTO>> GetAllAsync();

}