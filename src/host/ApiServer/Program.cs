using Framework.Infrastructure;
using Framework.Infrastructure.Logging.Serilog;
using Serilog;
using ApiServer;

StaticLogger.EnsureInitialized();
Log.Information("server booting up..");
try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.ConfigureFramework();
    builder.RegisterModules();

    WebApplication app = builder.Build();
    app.UseFramework();
    app.UseModules();
    await app.RunAsync();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex.Message, "unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("server shutting down..");
    await Log.CloseAndFlushAsync();
}
