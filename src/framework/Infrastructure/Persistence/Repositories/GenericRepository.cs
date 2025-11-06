using System.Linq.Expressions;
using Framework.Core.Domain;
using Framework.Core.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure.Persistence.Repositories;
public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
        where T : AuditableEntity<int>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<List<T>> GetByForeignKeyAsync(
        Expression<Func<T, int>> foreignKeySelector,
        int value,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>()
                      .Where(e => EF.Property<int>(e, GetPropertyName(foreignKeySelector)) == value)
                      .ToListAsync(cancellationToken);
    }

    private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        return ((MemberExpression)expression.Body).Member.Name;
    }
}
