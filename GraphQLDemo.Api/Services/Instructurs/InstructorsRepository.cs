using GraphQLDemo.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Api.Services.Instructors;

public class InstructorsRepository : IInstructorsRepository
{
    private readonly IDbContextFactory<SchoolDbContext> _schoolDbContext;

    public InstructorsRepository(IDbContextFactory<SchoolDbContext> schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }

    public async Task<InstructorDTO> CreateAsync(InstructorDTO instructor)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        await context.Instructors.AddAsync(instructor);
        await context.SaveChangesAsync();
        return instructor;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        var instructor = await context.Instructors.FirstOrDefaultAsync(c => c.Id == id);
        context.Instructors.Remove(instructor!);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<InstructorDTO>> GetAllAsync()
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Instructors.ToListAsync();

    }

    public async Task<InstructorDTO> GetByIdAsync(Guid id)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Instructors.FirstOrDefaultAsync(c => c.Id == id) ?? null!;
    }

    public async Task<IEnumerable<InstructorDTO>> GetManyByIdsAsync(IReadOnlyList<Guid> instructorIds)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Instructors.Where(i => instructorIds.Contains(i.Id)).ToListAsync();
    }

    public async Task<bool> IsExistAsync(Guid id)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        return await context.Instructors.AnyAsync(c => c.Id == id);
    }

    public async Task<InstructorDTO> UpdateAsync(InstructorDTO instructor)
    {
        var context = await _schoolDbContext.CreateDbContextAsync();
        context.Instructors.Update(instructor);
        await context.SaveChangesAsync();
        return  instructor;
    }
}