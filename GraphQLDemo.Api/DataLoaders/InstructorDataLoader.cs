using GraphQLDemo.Api.DTOs;
using GraphQLDemo.Api.Services.Instructors;

namespace GraphQLDemo.Api.DataLoaders;

public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDTO>
{
    private readonly IInstructorsRepository _instructorsRepository;
    public InstructorDataLoader(
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null,
        IInstructorsRepository instructorsRepository = null!) : base(batchScheduler, options)
    {
        _instructorsRepository = instructorsRepository;
    }

    protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        var instructorDTOs = await _instructorsRepository.GetManyByIdsAsync(keys);
        return instructorDTOs.ToDictionary(i => i.Id);
    }
}