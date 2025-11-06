using Framework.Core.Audit;
using Framework.Core.Identity.Roles;
using Framework.Core.Identity.Tokens;
using Framework.Core.Identity.Users.Abstractions;
using Framework.Infrastructure.Auth;
using Framework.Infrastructure.Identity.Roles;
using Framework.Infrastructure.Identity.Tokens;
using Framework.Infrastructure.Identity.Users.Services;
using Framework.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Framework.Infrastructure.Identity.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Framework.Core.Persistence;
using Framework.Infrastructure.Identity.Users.Endpoints;
using Framework.Infrastructure.Identity.Audit;
using Framework.Core.Identity.Users;
using Framework.Infrastructure.Identity.Tokens.Endpoints;

namespace Framework.Infrastructure.Identity;
internal static class IdentityExtensions
{
    internal static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddScoped<CurrentUserMiddleware>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IAuditService, AuditService>();
        services.BindDbContext<IdentityDbContext>();
        services.AddScoped<IDbInitializer, IdentityDbInitializer>();
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
        })
           .AddEntityFrameworkStores<IdentityDbContext>()
           .AddDefaultTokenProviders();
        return services;
    }

    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var users = app.MapGroup("users").WithTags("users");
        users.MapUserEndpoints();

        var tokens = app.MapGroup("token").WithTags("Token");
        tokens.MapTokenEndpoints();

        var roles = app.MapGroup("roles").WithTags("roles");
        //roles.MapRoleEndpoints();

        return app;
    }
}
