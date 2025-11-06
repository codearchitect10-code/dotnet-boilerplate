namespace Framework.Core.Masters.Abstractions;
public interface IBusinessActivityService
{
    Task<List<BusinessActivity>> GetAllAsync(CancellationToken cancellationToken);
    Task<BusinessActivity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<BusinessActivity>> GetByBusinessSubSectorIdAsync(int subSectorId, CancellationToken cancellationToken);
}
