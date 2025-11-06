using Framework.Core.Exceptions;
using Framework.Core.Identity.Users.Abstractions;
using Framework.Core.Identity.Users.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Framework.Core.Identity.Users;
using Framework.Core.Caching;
using Framework.Infrastructure.Identity.Persistence;

namespace Framework.Infrastructure.Identity.Users.Services;
internal sealed partial class UserService(
     UserManager<ApplicationUser> userManager,
     RoleManager<IdentityRole> roleManager,
     IdentityDbContext db,
     ICacheService cache
    ) : IUserService
{
    public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
    {
        return await userManager.FindByEmailAsync(email.Normalize()) is ApplicationUser user && user.Id != exceptId;
    }

    public async Task<bool> ExistsWithNameAsync(string name)
    {
        return await userManager.FindByNameAsync(name) is not null;
    }

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null)
    {
        return await userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is ApplicationUser user && user.Id != exceptId;
    }

    public async Task<UserDetail> GetAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("user not found");

        return user.Adapt<UserDetail>();
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        return await userManager.Users.AsNoTracking().CountAsync(cancellationToken);
    }

    public async Task<List<UserDetail>> GetListAsync(CancellationToken cancellationToken)
    {
        var users = await userManager.Users.AsNoTracking().ToListAsync(cancellationToken);
        return users.Adapt<List<UserDetail>>();
    }

}
