using GraphQLDemo.Api.DTOs;

namespace GraphQLDemo.Api.Services.Students;

public interface IStudentsRepository
{
    // Mutations/Commands
    Task<StudentDTO> CreateAsync(StudentDTO student);
    Task<StudentDTO> UpdateAsync(StudentDTO student);
    Task<bool> DeleteAsync(Guid id);

    // Queries
    Task<bool> IsExistAsync(Guid id);
    Task<StudentDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<StudentDTO>> GetManyByIdsAsync(IReadOnlyList<Guid> studentIds);

    Task<IEnumerable<StudentDTO>> GetAllAsync();

}