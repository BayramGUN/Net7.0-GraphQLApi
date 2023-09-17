using GraphQLDemo.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Api.Services.Courses;

public class CoursesRepository : ICoursesRepository
{
    private readonly IDbContextFactory<SchoolDbContext> _schoolDbContext;

    public CoursesRepository(IDbContextFactory<SchoolDbContext> schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }

    public async Task<CourseDTO> CreateAsync(CourseDTO course)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        await context.Courses.AddAsync(course);
        await context.SaveChangesAsync();
        return course;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        context.Courses.Remove(course!);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<CourseDTO>> GetAllAsync()
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Courses.Include(c => c.Instructor)
                                    .Include(c => c.Students)
                                    .ToListAsync();

    }

    public async Task<CourseDTO> GetByIdAsync(Guid id)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Courses.Include(c => c.Instructor)
                                    .Include(c => c.Students)
                                    .FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public async Task<bool> IsExistAsync(Guid id)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Courses.AnyAsync(c => c.Id == id);
    }

    public async Task<CourseDTO> UpdateAsync(CourseDTO course)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        context.Courses.Update(course);
        await context.SaveChangesAsync();
        return  course;
    }
}