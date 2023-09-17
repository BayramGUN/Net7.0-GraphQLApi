using GraphQLDemo.Api.DTOs;

namespace GraphQLDemo.Api.Services.Instructors;

public interface IInstructorsRepository
{
    // Mutations/Commands
    Task<InstructorDTO> CreateAsync(InstructorDTO instructor);
    Task<InstructorDTO> UpdateAsync(InstructorDTO instructor);
    Task<bool> DeleteAsync(Guid id);

    // Queries
    Task<bool> IsExistAsync(Guid id);
    Task<InstructorDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<InstructorDTO>> GetAllAsync();
    Task<IEnumerable<InstructorDTO>> GetManyByIds(IReadOnlyList<Guid> instructorIds);
}