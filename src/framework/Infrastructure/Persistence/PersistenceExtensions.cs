using Framework.Core.Persistence;
using Framework.Core.Shared.Interfaces;
using Framework.Infrastructure.Persistence.Interceptors;
using Framework.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

namespace Framework.Infrastructure.Persistence;
public static class PersistenceExtensions
{
    private static readonly ILogger Logger = Log.ForContext(typeof(PersistenceExtensions));

    public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        Logger.Information("Registering Database services...");


        builder.Services.AddOptions<DatabaseOptions>()
           .BindConfiguration(nameof(DatabaseOptions))
           .ValidateDataAnnotations()
           .PostConfigure(config =>
           {
               Logger.Information("current db provider: {DatabaseProvider}", config.Provider);
           });

        builder.Services.BindDbContext<ApplicationDbContext>();
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        Logger.Information("Database services registered successfully.");
        return builder;
    }

    public static IServiceCollection BindDbContext<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<TContext>((sp, options) =>
        {
            var dbConfig = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.ConfigureDatabase(dbConfig.Provider, dbConfig.ConnectionString);
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });
        return services;
    }

    internal static DbContextOptionsBuilder ConfigureDatabase(this DbContextOptionsBuilder builder, string dbProvider, string connectionString)
    {
        builder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
        return dbProvider.ToUpperInvariant() switch
        {
            DbProviders.MSSQL => builder.UseSqlServer(connectionString, e =>
                                e.MigrationsAssembly("MSSQL")),
            _ => throw new InvalidOperationException($"DB Provider {dbProvider} is not supported."),
        };
    }

    public static IApplicationBuilder SetupDatabase(this IApplicationBuilder app)
    {
        // create a scope
        using var dbProviderScope = app.ApplicationServices.CreateScope();

        // fetch all services that implement IDbInitializer (we can have many, e.g. IdentityDbInitializer, MastersDbInitializer, etc.)
        var initializers = dbProviderScope.ServiceProvider.GetServices<IDbInitializer>();
        foreach (var initializer in initializers)
        {
            // apply pending migrations
            initializer.MigrateAsync(CancellationToken.None).Wait();

            // run seed data
            initializer.SeedAsync(CancellationToken.None).Wait();
        }
        return app;
    }
}
