namespace Framework.Core.Masters.Abstractions;
public interface IBusinessSectorService
{
    Task<List<BusinessSector>> GetAllAsync(CancellationToken cancellationToken);
    Task<BusinessSector?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
