using Framework.Core.Masters;
using Framework.Core.Masters.Abstractions;
using Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Framework.Infrastructure.Masters.Endpoints;
public static class BusinessActivityEndpoints
{
    internal static IEndpointRouteBuilder MapBusinessActivityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("business-activities")
            .WithTags("BusinessActivities");

        group.MapGet("/", async (IBusinessActivityService service, CancellationToken ct) =>
            Results.Ok(await service.GetAllAsync(ct)))
        .WithName("GetAllBusinessActivity")
        .WithSummary("Get all Business Activities")
        .WithDescription("Returns a list of all Business Activities")
        .Produces<List<BusinessActivity>>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.BusinessActivity.View")
        .MapToApiVersion(1);

        group.MapGet("/{id:int}", async (IBusinessActivityService service, int id, CancellationToken ct) =>
            Results.Ok(await service.GetByIdAsync(id, ct)))
        .WithName("GetBusinessActivityById")
        .WithSummary("Get Business Activity by ID")
        .WithDescription("Returns a Business Activity by its unique ID")
        .Produces<BusinessActivity>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.BusinessActivity.View")
        .MapToApiVersion(1);

        group.MapGet("/by-subsector/{subSectorId:int}", async (IBusinessActivityService service, int subSectorId, CancellationToken ct) =>
            Results.Ok(await service.GetByBusinessSubSectorIdAsync(subSectorId, ct)))
        .WithName("GetBusinessActivityBySubSectorId")
        .WithSummary("Get Activities by SubSector ID")
        .WithDescription("Returns all Business Activities belonging to a specific SubSector")
        .Produces<List<BusinessActivity>>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.BusinessActivity.View")
        .MapToApiVersion(1);

        return group;
    }
}
