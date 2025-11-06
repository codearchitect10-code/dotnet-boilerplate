using Framework.Core.Masters.Abstractions;
using Framework.Core.Masters;
using Framework.Core.Shared.Interfaces;

namespace Framework.Infrastructure.Masters.Services;
public class EmiratesRegionService(IGenericRepository<EmiratesRegion> repo) : IEmiratesRegionService
{
    private readonly IGenericRepository<EmiratesRegion> _repo = repo;

    public Task<List<EmiratesRegion>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _repo.GetAllAsync(cancellationToken);
    }
    public Task<EmiratesRegion?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _repo.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<EmiratesRegion>> GetByEmiratesIdAsync(int emiratesId, CancellationToken cancellationToken)
    {
        return await _repo.GetByForeignKeyAsync(x => x.EmiratesId, emiratesId, cancellationToken);
    }
}
