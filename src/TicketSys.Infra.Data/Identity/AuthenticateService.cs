using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketSys.Domain.Account;
using TicketSys.Domain.Entities;

namespace TicketSys.Infra.Data.Identity;

public class AuthenticateService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole> roleManager) : IAuthenticate
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<bool> Authenticate(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(
            email,
            password,
            false,
            lockoutOnFailure: false
            );

        return result.Succeeded;
    }

    public Task<IEnumerable<Domain.Entities.ApplicationUser>> GetAllAsync()
    {
        var usersList = _userManager.Users.ToList();

        var mapped = usersList.Select(u => new Domain.Entities.ApplicationUser()
        {
            Id = u.Id,
            Email = u.Email ?? string.Empty,
            Name = u.Name,
            IsActive = u.IsActive,

        });

        return Task.FromResult(mapped.AsEnumerable());
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> RegisterUser(string email, string password, string name, string idRole)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            Name = name,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return false;

        var roles = await GetRoleByIdAsync(idRole);

        await _userManager.AddToRoleAsync(user, roles?.Description!);
        return result.Succeeded;
    }

    public async Task<bool> UpdateUser(string id, string email, string? password, string name, bool isActive, string idRole)
    {
        var existingUser = await _userManager.FindByIdAsync(id);
        if (existingUser == null)
            return false;

        existingUser.Email = email;
        existingUser.UserName = email;
        existingUser.Name = name;
        existingUser.IsActive = isActive;

        if (!string.IsNullOrWhiteSpace(password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            var passwordResult = await _userManager.ResetPasswordAsync(existingUser, token, password);
            if (!passwordResult.Succeeded)
                return false;
        }

        var currentRoles = await _userManager.GetRolesAsync(existingUser);
        if (currentRoles.Any())
            await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);

        var newRole = await GetRoleByIdAsync(idRole);
        if (newRole != null)
            await _userManager.AddToRoleAsync(existingUser, newRole.Description);

        var result = await _userManager.UpdateAsync(existingUser);
        return result.Succeeded;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        var rolesList = await _roleManager.Roles.AsNoTracking().ToListAsync();

        var mapped = rolesList.Select(r => new Role(
            r.Id,
            r.Name ?? string.Empty
        ));

        return mapped.AsEnumerable();
    }

    public async Task<Domain.Entities.ApplicationUser?> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return null;

        return new Domain.Entities.ApplicationUser()
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            Name = user.Name,
            IsActive = user.IsActive,
            TypeOfAccountId = GetRoleIdByUserAsync(user.Id).Result
        };

    }

    public async Task<Role?> GetRoleByIdAsync(string id)
    {
        var role = await _roleManager.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        if (role == null)
            return null;

        return new Role(
            role.Id,
            role.Name ?? string.Empty
        );
    }

    public async Task<string?> GetRoleIdByUserAsync(string userId)
    {

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return null;

        var roles = await _userManager.GetRolesAsync(user);
        var roleName = roles.FirstOrDefault();

        if (roleName == null)
            return null;

        var role = await _roleManager.FindByNameAsync(roleName);

        return role?.Id;
    }
}
