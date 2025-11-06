namespace Framework.Core.Masters.Abstractions;
public interface IEmiratesService
{
    Task<List<Emirates>> GetAllAsync(CancellationToken cancellationToken);
    Task<Emirates?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
