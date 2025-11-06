using Framework.Core.Origin;
using Framework.Core.Persistence;
using Framework.Infrastructure.Identity.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Framework.Core.Shared.Authorization;
using Framework.Core.Identity.Users;

namespace Framework.Infrastructure.Identity.Persistence;
internal class IdentityDbInitializer(
    ILogger<IdentityDbInitializer> logger,
    IdentityDbContext context,
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager,
    TimeProvider timeProvider,
    IOptions<OriginOptions> originSettings) : IDbInitializer
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken).ConfigureAwait(false)).Any())
        {
            await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("Applied database migrations for identity module");
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        await SeedRolesAsync();
        await SeedAdminUserAsync();
    }

    private async Task SeedRolesAsync()
    {
        foreach (string roleName in RolesDefault.DefaultRoles)
        {
            if (await roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not IdentityRole role)
            {
                // create role
                role = new IdentityRole(roleName);
                await roleManager.CreateAsync(role);
            }

            // Assign permissions
            if (roleName == RolesDefault.Basic)
            {
                await AssignPermissionsToRoleAsync(context, Permissions.Basic, role);
            }
            else if (roleName == RolesDefault.Admin)
            {
                await AssignPermissionsToRoleAsync(context, Permissions.Admin, role);
            }
        }
    }

    private async Task AssignPermissionsToRoleAsync(IdentityDbContext dbContext, IReadOnlyList<Permission> permissions, IdentityRole role)
    {
        var currentClaims = await roleManager.GetClaimsAsync(role);
        var newClaims = permissions
            .Where(permission => !currentClaims.Any(c => c.Type == Claims.Permission && c.Value == permission.Name))
            .Select(permission => new RoleClaim
            {
                RoleId = role.Id,
                ClaimType = Claims.Permission,
                ClaimValue = permission.Name,
                CreatedBy = "application",
                CreatedOn = timeProvider.GetUtcNow()
            })
            .ToList();

        foreach (var claim in newClaims)
        {
            logger.LogInformation("Seeding {Role} Permission '{Permission}'", role.Name, claim.ClaimValue);
            await dbContext.RoleClaims.AddAsync(claim);
        }

        // Save changes to the database context
        if (newClaims.Count != 0)
        {
            await dbContext.SaveChangesAsync();
        }

    }

    private async Task SeedAdminUserAsync()
    {
        const string adminEmail = "admin@example.com";
        const string defaultPassword = "Admin@123";

        if (await userManager.Users.FirstOrDefaultAsync(u => u.Email == adminEmail) is not { } adminUser)
        {
            adminUser = new ApplicationUser
            {
                FirstName = "System",
                LastName = RolesDefault.Admin,
                Email = adminEmail,
                UserName = adminEmail,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = adminEmail.ToUpperInvariant(),
                NormalizedUserName = adminEmail.ToUpperInvariant(),
                ImageUrl = new Uri(originSettings.Value.OriginUrl! + "/default-profile.png"),
                IsActive = true
            };

            logger.LogInformation("Seeding Default Admin User");
            var password = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = password.HashPassword(adminUser, defaultPassword);
            await userManager.CreateAsync(adminUser);
        }

        // Assign role to user
        if (!await userManager.IsInRoleAsync(adminUser, RolesDefault.Admin))
        {
            logger.LogInformation("Assigning Admin Role to Admin User");
            await userManager.AddToRoleAsync(adminUser, RolesDefault.Admin);
        }
    }
}
