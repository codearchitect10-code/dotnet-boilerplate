namespace Framework.Core.Masters.Abstractions;
public interface IBusinessSubSectorService
{
    Task<List<BusinessSubSector>> GetAllAsync(CancellationToken cancellationToken);
    Task<BusinessSubSector?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<BusinessSubSector>> GetByBusinessSectorIdAsync(int sectorId, CancellationToken cancellationToken);
}
