using Framework.Core.Exceptions;
using Framework.Core.Identity.Roles;
using Framework.Core.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using Framework.Infrastructure.Identity.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure.Identity.Roles;
internal class RoleService(
    RoleManager<IdentityRole> roleManager,
    IdentityDbContext context) : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task DeleteRoleAsync(string id)
    {
        IdentityRole? role = await _roleManager.FindByIdAsync(id);

        _ = role ?? throw new NotFoundException("role not found");

        await _roleManager.DeleteAsync(role);
    }

    public async Task<RoleDto?> GetRoleAsync(string id)
    {
        IdentityRole? role = await _roleManager.FindByIdAsync(id);

        _ = role ?? throw new NotFoundException("role not found");

        return new RoleDto { Id = role.Id, Name = role.Name!};
    }

    public async Task<IEnumerable<RoleDto>> GetRolesAsync()
    {
        return await Task.Run(() => _roleManager.Roles
            .Select(role => new RoleDto { Id = role.Id, Name = role.Name!})
            .ToList());
    }

    public async Task<RoleDto> GetWithPermissionsAsync(string id, CancellationToken cancellationToken)
    {
        var role = await GetRoleAsync(id);
        _ = role ?? throw new NotFoundException("role not found");

        role.Permissions = await context.RoleClaims
            .Where(c => c.RoleId == id && c.ClaimType == Claims.Permission)
            .Select(c => c.ClaimValue!)
            .ToListAsync(cancellationToken);

        return role;
    }
}
