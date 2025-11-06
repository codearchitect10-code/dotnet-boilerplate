using Framework.Core.Masters.Abstractions;
using Framework.Core.Masters;
using Framework.Core.Shared.Interfaces;

namespace Framework.Infrastructure.Masters.Services;
public class BusinessSubSectorService(IGenericRepository<BusinessSubSector> repo) : IBusinessSubSectorService
{
    private readonly IGenericRepository<BusinessSubSector> _repo = repo;

    public Task<List<BusinessSubSector>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _repo.GetAllAsync(cancellationToken);
    }

    public Task<BusinessSubSector?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _repo.GetByIdAsync(id, cancellationToken);
    }

    public Task<List<BusinessSubSector>> GetByBusinessSectorIdAsync(int sectorId, CancellationToken cancellationToken)
    {
        return _repo.GetByForeignKeyAsync(x => x.BusinessSectorId, sectorId, cancellationToken);
    }
}
