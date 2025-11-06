using System;
using System.Collections.Generic;
using Framework.Core.Masters.Abstractions;
using Framework.Infrastructure.Masters.Endpoints;
using Framework.Infrastructure.Masters.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Infrastructure.Masters;
internal static class MastersExtensions
{
    internal static IServiceCollection ConfigureMaster(this IServiceCollection services)
    {
        services.AddScoped<IBusinessSectorService, BusinessSectorService>();
        services.AddScoped<IBusinessSubSectorService, BusinessSubSectorService>();
        services.AddScoped<IBusinessActivityService, BusinessActivityService>();
        services.AddScoped<IEmiratesService, EmiratesService>();
        services.AddScoped<IEmiratesRegionService, EmiratesRegionService>();

        return services;
    }

    public static IEndpointRouteBuilder MapMastersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapBusinessSectorEndpoints();
        app.MapBusinessSubSectorEndpoints();
        app.MapBusinessActivityEndpoints();
        app.MapEmiratesEndpoints();
        app.MapEmiratesRegionEndpoints();

        return app;
    }
}
