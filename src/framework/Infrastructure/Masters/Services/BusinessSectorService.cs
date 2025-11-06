using Framework.Core.Masters;
using Framework.Core.Masters.Abstractions;
using Framework.Core.Shared.Interfaces;

namespace Framework.Infrastructure.Masters.Services;
public class BusinessSectorService(IGenericRepository<BusinessSector> repo) : IBusinessSectorService
{
    private readonly IGenericRepository<BusinessSector> _repo = repo;

    public Task<List<BusinessSector>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _repo.GetAllAsync(cancellationToken);
    }

    public Task<BusinessSector?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _repo.GetByIdAsync(id, cancellationToken);
    }
}
