using Framework.Core.Masters;
using Framework.Core.Masters.Abstractions;
using Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Framework.Infrastructure.Masters.Endpoints;
public static class BusinessSubSectorEndpoints
{
    internal static IEndpointRouteBuilder MapBusinessSubSectorEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("businesssubsectors")
            .WithTags("BusinessSubSectors");

        group.MapGet("/", async (IBusinessSubSectorService service, CancellationToken ct) =>
            Results.Ok(await service.GetAllAsync(ct)))
        .WithName("GetAllBusinessSubSectors")
        .Produces<List<BusinessSubSector>>(StatusCodes.Status200OK)
        .WithSummary("Get all business sub-sectors")
        .WithDescription("Returns a list of all business sub-sectors")
        .RequirePermission("Permissions.Masters.BusinessSubSector.View")
        .MapToApiVersion(1);

        group.MapGet("/{id:int}", async (int id, IBusinessSubSectorService service, CancellationToken ct) =>
        {
            var subSector = await service.GetByIdAsync(id, ct);
            return subSector is not null ? Results.Ok(subSector) : Results.NotFound();
        })
        .WithName("GetBusinessSubSectorById")
        .Produces<BusinessSubSector>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Get a business sub sector by ID")
        .WithDescription("Returns a single business sub sector if found, otherwise 404 Not Found")
        .RequirePermission("Permissions.Masters.BusinessSubSector.View")
        .MapToApiVersion(1);

        group.MapGet("/by-sector/{sectorId:int}", async (int sectorId, IBusinessSubSectorService service, CancellationToken ct) =>
            Results.Ok(await service.GetByBusinessSectorIdAsync(sectorId, ct)))
        .WithName("GetBusinessSubSectorsBySectorId")
        .Produces<List<BusinessSubSector>>(StatusCodes.Status200OK)
        .WithSummary("Get business sub sectors by sector ID")
        .WithDescription("Returns all business sub sectors belonging to a given sector")
        .RequirePermission("Permissions.Masters.BusinessSubSector.View")
        .MapToApiVersion(1);


        return group;
    }
}
