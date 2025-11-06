using Framework.Core.Masters;
using Framework.Core.Masters.Abstractions;
using Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Framework.Infrastructure.Masters.Endpoints;
public static class EmiratesRegionEndpoints
{
    internal static IEndpointRouteBuilder MapEmiratesRegionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("emirates-regions")
            .WithTags("EmiratesRegions");

        group.MapGet("/", async (IEmiratesRegionService service, CancellationToken ct) =>
            Results.Ok(await service.GetAllAsync(ct)))
        .WithName("GetAllEmiratesRegion")
        .WithSummary("Get all Emirates Regions")
        .WithDescription("Returns a list of all Emirates Regions")
        .Produces<List<EmiratesRegion>>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.EmiratesRegion.View")
        .MapToApiVersion(1);

        group.MapGet("/{id:int}", async (IEmiratesRegionService service, int id, CancellationToken ct) =>
            Results.Ok(await service.GetByIdAsync(id, ct)))
        .WithName("GetAllEmiratesRegionById")
        .WithSummary("Get Emirates Region by ID")
        .WithDescription("Returns an Emirates Region by its unique ID")
        .Produces<EmiratesRegion>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.EmiratesRegion.View")
        .MapToApiVersion(1);

        group.MapGet("/by-emirates/{emiratesId:int}", async (IEmiratesRegionService service, int emiratesId, CancellationToken ct) =>
            Results.Ok(await service.GetByEmiratesIdAsync(emiratesId, ct)))
        .WithName("GetAllEmiratesRegionByEmiratesId")
        .WithSummary("Get Regions by Emirates ID")
        .WithDescription("Returns all Emirates Regions belonging to a specific Emirates")
        .Produces<List<EmiratesRegion>>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.EmiratesRegion.View")
        .MapToApiVersion(1);

        return group;
    }
}
