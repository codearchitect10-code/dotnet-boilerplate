using Framework.Core.Masters;
using Framework.Core.Masters.Abstractions;
using Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Framework.Infrastructure.Masters.Endpoints;
public static class BusinessSectorEndpoints
{
    internal static IEndpointRouteBuilder MapBusinessSectorEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("businesssectors")
            .WithTags("BusinessSectors");

        group.MapGet("/", async (IBusinessSectorService service, CancellationToken ct) =>
            Results.Ok(await service.GetAllAsync(ct)))
        .WithName("GetAllBusinessSectors")
        .WithSummary("Get all Business Sectors")
        .WithDescription("Returns a list of all Business Sectors")
        .Produces<List<BusinessSector>>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.BusinessSector.View")
        .MapToApiVersion(1);

        group.MapGet("/{id:int}", async (IBusinessSectorService service, int id, CancellationToken ct) =>
            Results.Ok(await service.GetByIdAsync(id, ct)))
        .WithName("GetBusinessSectorById")
        .WithSummary("Get Business Sector by ID")
        .WithDescription("Returns a Business Sector by its unique ID")
        .Produces<BusinessSector>(StatusCodes.Status200OK)
        .RequirePermission("Permissions.Masters.BusinessSector.View")
        .MapToApiVersion(1);
        return group;
    }
}
