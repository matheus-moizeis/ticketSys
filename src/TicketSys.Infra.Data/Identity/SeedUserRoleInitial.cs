using Microsoft.AspNetCore.Identity;
using TicketSys.Domain.Account;

namespace TicketSys.Infra.Data.Identity;

public class SeedUserRoleInitial(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public void SeedUsers()
    {
        var emailSolicitor = Environment.GetEnvironmentVariable("USER_SOLICITOR_DEFAULT_EMAIL")?.ToString();
        if (_userManager.FindByEmailAsync(emailSolicitor!).Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = emailSolicitor,
                Email = emailSolicitor,
                NormalizedUserName = emailSolicitor!.ToUpper(),
                NormalizedEmail = emailSolicitor!.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = Environment.GetEnvironmentVariable("USER_SOLICITOR_DEFAULT_NAME")?.ToString(),
                IsActive = true
            };

            var result = _userManager.CreateAsync(user, Environment.GetEnvironmentVariable("USER_SOLICITOR_DEFAULT_PASSWORD")!.ToString()).Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Solicitante").Wait();
            }

            var emailAdmin = Environment.GetEnvironmentVariable("USER_ADMIN_DEFAULT_EMAIL")?.ToString();

            ApplicationUser adm = new()
            {
                UserName = emailAdmin,
                Email = emailAdmin,
                NormalizedUserName = emailAdmin!.ToUpper(),
                NormalizedEmail = emailAdmin!.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = Environment.GetEnvironmentVariable("USER_ADMIN_DEFAULT_NAME")?.ToString(),
                IsActive = true
            };

            result = _userManager.CreateAsync(adm, Environment.GetEnvironmentVariable("USER_ADMIN_DEFAULT_PASSWORD")!.ToString()).Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(adm, "Admin").Wait();
            }
        }
    }

    public void SeedRoles()
    {

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            _ = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Solicitante").Result)
        {
            IdentityRole role = new()
            {
                Name = "Solicitante",
                NormalizedName = "SOLICITANTE"
            };

            _ = _roleManager.CreateAsync(role).Result;
        }
    }
}
