using System.Linq.Expressions;
using Framework.Core.Domain;

namespace Framework.Core.Shared.Interfaces;
public interface IGenericRepository<T> where T : AuditableEntity<int>
{
    // Get all records
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    // Get by primary key (Id)
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    // Get by foreign key
    Task<List<T>> GetByForeignKeyAsync(
        Expression<Func<T, int>> foreignKeySelector,
        int value,
        CancellationToken cancellationToken = default);
}
