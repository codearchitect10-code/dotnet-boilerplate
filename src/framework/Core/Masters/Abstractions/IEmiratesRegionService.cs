namespace Framework.Core.Masters.Abstractions;
public interface IEmiratesRegionService
{
    Task<List<EmiratesRegion>> GetAllAsync(CancellationToken cancellationToken);
    Task<EmiratesRegion?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<EmiratesRegion>> GetByEmiratesIdAsync(int emiratesId, CancellationToken cancellationToken);
}
