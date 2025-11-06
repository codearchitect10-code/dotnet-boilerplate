using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Framework.Infrastructure.Identity.Users.Endpoints;
internal static class UserEndpointExtensions
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapRegisterUserEndpoint();
    //    app.MapSelfRegisterUserEndpoint();
    //    app.MapUpdateUserEndpoint();
    //    app.MapGetUsersListEndpoint();
    //    app.MapDeleteUserEndpoint();
    //    app.MapForgotPasswordEndpoint();
    //    app.MapChangePasswordEndpoint();
    //    app.MapResetPasswordEndpoint();
    //    app.MapGetMeEndpoint();
    //    app.MapGetUserEndpoint();
    //    app.MapGetCurrentUserPermissionsEndpoint();
    //    app.ToggleUserStatusEndpointEndpoint();
    //    app.MapAssignRolesToUserEndpoint();
    //    app.MapGetUserRolesEndpoint();
    //    app.MapGetUserAuditTrailEndpoint();
    //    app.MapConfirmEmailEndpoint();
        return app;
    }
}
