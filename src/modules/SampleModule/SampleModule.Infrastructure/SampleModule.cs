using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace SampleModule.Infrastructure;

public static class SampleModule
{
    public class Endpoints : CarterModule
    {
        public Endpoints() : base("SampleModule") { }
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            //var productGroup = app.MapGroup("products").WithTags("products");
            //productGroup.MapProductCreationEndpoint();
            //productGroup.MapGetProductEndpoint();
            //productGroup.MapGetProductListEndpoint();
            //productGroup.MapProductUpdateEndpoint();
            //productGroup.MapProductDeleteEndpoint();

            //var brandGroup = app.MapGroup("brands").WithTags("brands");
            //brandGroup.MapBrandCreationEndpoint();
            //brandGroup.MapGetBrandEndpoint();
            //brandGroup.MapGetBrandListEndpoint();
            //brandGroup.MapBrandUpdateEndpoint();
            //brandGroup.MapBrandDeleteEndpoint();
        }
    }

    public static WebApplicationBuilder RegisterSampleModuleServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        //register module services here
        return builder;
    }
    public static WebApplication UseSampleModule(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        //use module services here
        return app;
    }
}
