using GraphQLDemo.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Api.Services.Students;

public class StudentsRepository : IStudentsRepository
{
    private readonly IDbContextFactory<SchoolDbContext> _schoolDbContext;

    public StudentsRepository(IDbContextFactory<SchoolDbContext> schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }

    public async Task<StudentDTO> CreateAsync(StudentDTO student)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        await context.Students.AddAsync(student);
        await context.SaveChangesAsync();
        return student;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        var student = await context.Students.FirstOrDefaultAsync(c => c.Id == id);
        context.Students.Remove(student!);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<StudentDTO>> GetAllAsync()
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Students.Include(s => s.Courses)!.ThenInclude(c => c.Instructor).ToListAsync();
    }

    public async Task<StudentDTO> GetByIdAsync(Guid id)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Students.Include(s => s.Courses).FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public async Task<IEnumerable<StudentDTO>> GetManyByIdsAsync(IReadOnlyList<Guid> studentIds)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Students.Where(i => studentIds.Contains(i.Id)).ToListAsync();
    }

    public async Task<bool> IsExistAsync(Guid id)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Students.AnyAsync(c => c.Id == id);
    }

    public async Task<StudentDTO> UpdateAsync(StudentDTO student)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        context.Students.Update(student);
        await context.SaveChangesAsync();
        return  student;
    }
}