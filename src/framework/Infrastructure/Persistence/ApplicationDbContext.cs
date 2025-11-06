using Framework.Core.Domain;
using Framework.Core.Domain.Contracts;
using Framework.Core.Masters;
using Framework.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Framework.Infrastructure.Persistence;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
    IPublisher publisher,
    IOptions<DatabaseOptions> settings) : DbContext(options)
{
    private readonly IPublisher _publisher = publisher;
    private readonly DatabaseOptions _settings = settings.Value;

    public DbSet<BusinessSector> BusinessSectors { get; set; }
    public DbSet<BusinessSubSector> BusinessSubSectors { get; set; }
    public DbSet<BusinessActivity> BusinessActivities { get; set; }
    public DbSet<Emirates> Emirates { get; set; }
    public DbSet<EmiratesRegion> EmiratesRegions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AppendGlobalQueryFilter<ISoftDeletable>(s => s.Deleted == null);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Apply GETDATE() default for Created in ALL AuditableEntity<T>
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            // Check if entity inherits from AuditableEntity<> (any TId)
            var baseType = clrType.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(AuditableEntity<>))
                {
                    var createdProp = entityType.FindProperty(nameof(AuditableEntity<int>.Created));
                    createdProp?.SetDefaultValueSql("GETDATE()");
                    break;
                }
                baseType = baseType.BaseType;
            }
        }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();

        if (!string.IsNullOrWhiteSpace(_settings.ConnectionString))
        {
            optionsBuilder.ConfigureDatabase(_settings.Provider, _settings.ConnectionString);
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await PublishDomainEventsAsync().ConfigureAwait(false);
        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .SelectMany(e =>
            {
                var domainEvents = e.DomainEvents.ToList();
                e.DomainEvents.Clear();
                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}
