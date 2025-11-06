using Framework.Core.Masters.Abstractions;
using Framework.Core.Masters;
using Framework.Core.Shared.Interfaces;

namespace Framework.Infrastructure.Masters.Services;
public class BusinessActivityService(IGenericRepository<BusinessActivity> repo) : IBusinessActivityService
{
    private readonly IGenericRepository<BusinessActivity> _repo = repo;

    public Task<List<BusinessActivity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _repo.GetAllAsync(cancellationToken);
    }

    public Task<BusinessActivity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _repo.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<BusinessActivity>> GetByBusinessSubSectorIdAsync(int subSectorId, CancellationToken cancellationToken)
    {
        return await _repo.GetByForeignKeyAsync(x => x.BusinessSubSectorId, subSectorId, cancellationToken);
    }
}
