using Framework.Core.Masters;
using Framework.Core.Masters.Abstractions;
using Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Framework.Infrastructure.Masters.Endpoints;
public static class EmiratesEndpoints
{
    internal static IEndpointRouteBuilder MapEmiratesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("emirates")
            .WithTags("Emirates");

        group.MapGet("/", async (IEmiratesService service, CancellationToken ct) =>
            Results.Ok(await service.GetAllAsync(ct)))
        .WithName("GetAllEmirates")
        .WithSummary("Get all Emirates")
        .WithDescription("Returns a list of all Emirates")
        .Produces<List<Emirates>>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.Emirates.View")
        .MapToApiVersion(1);

        group.MapGet("/{id:int}", async (IEmiratesService service, int id, CancellationToken ct) =>
            Results.Ok(await service.GetByIdAsync(id, ct)))
        .WithName("GetAllEmiratesById")
        .WithSummary("Get Emirates by ID")
        .WithDescription("Returns an Emirates record by its unique ID")
        .Produces<Emirates>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.Emirates.View")
        .MapToApiVersion(1);

        return group;
    }
}
