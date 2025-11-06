using System.Reflection;
using FluentValidation;
using SampleModule.Application;
using Carter;
using Asp.Versioning.Conventions;
using SampleModule.Infrastructure;

namespace ApiServer;

public static class ServerExtensions
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        //define module assemblies
        var assemblies = new Assembly[]
        {
            typeof(SampleModuleMetadata).Assembly,
        };

        //register validators
        builder.Services.AddValidatorsFromAssemblies(assemblies);

        //register mediatr
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
        });

        //register module services
        builder.RegisterSampleModuleServices();

        /*
        Why Use Carter Over Minimal APIs?
            - Carter offers better organization than raw Minimal APIs:
            - Group routes into modules(like ProductModule, UserModule).
            - Reusable across projects(just reference the module).
            - Cleaner than app.MapGet() sprawl.
        */
        builder.Services.AddCarter(configurator: config =>
        {
            config.WithModule<SampleModule.Infrastructure.SampleModule.Endpoints>();
        });

        return builder;
    }
    public static WebApplication UseModules(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseSampleModule();

        //register api versions
        var versions = app.NewApiVersionSet()
                    .HasApiVersion(1)
                    .HasApiVersion(2)
                    .ReportApiVersions()
                    .Build();

        //map versioned endpoint
        var endpoints = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(versions);

        //use carter
        endpoints.MapCarter();

        return app;
    }
}
