using Framework.Core.Masters;
using Framework.Core.Masters.Abstractions;
using Framework.Core.Shared.Interfaces;

namespace Framework.Infrastructure.Masters.Services;
public class EmiratesService(IGenericRepository<Emirates> repo) : IEmiratesService
{
    private readonly IGenericRepository<Emirates> _repo = repo;

    public Task<List<Emirates>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _repo.GetAllAsync(cancellationToken);
    }

    public Task<Emirates?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _repo.GetByIdAsync(id, cancellationToken);
    }
}
