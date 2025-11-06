using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Framework.Infrastructure.Identity.Users;
using Framework.Infrastructure.Identity.Roles;
using Framework.Core.Audit;
using Framework.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Framework.Infrastructure.Persistence;
using Framework.Core.Identity.Users;

namespace Framework.Infrastructure.Identity.Persistence;
public class IdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserClaim<string>,
    IdentityUserRole<string>,
    IdentityUserLogin<string>,
    RoleClaim,
    IdentityUserToken<string>>
{
    private readonly DatabaseOptions _settings;
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IOptions<DatabaseOptions> settings) : base(options)
    {
        _settings = settings.Value;
    }

    public DbSet<AuditTrail> AuditTrails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrWhiteSpace(_settings.ConnectionString))
        {
            optionsBuilder.ConfigureDatabase(_settings.Provider, _settings.ConnectionString);
        }
    }
}
