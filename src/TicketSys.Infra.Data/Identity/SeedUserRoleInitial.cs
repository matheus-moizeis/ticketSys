using System.Threading.Tasks;
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
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                NormalizedUserName = "USUARIO@LOCALHOST",
                NormalizedEmail = "USUARIO@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, "Numsey#2024").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Solicitante").Wait();
            }

            ApplicationUser adm = new()
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                NormalizedUserName = "USUARIO@LOCALHOST",
                NormalizedEmail = "USUARIO@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            result = _userManager.CreateAsync(user, "Numsey#2024").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Admin").Wait();
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

            var roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Solicitante").Result)
        {
            IdentityRole role = new()
            {
                Name = "Solicitante",
                NormalizedName = "SOLICITANTE"
            };

            var roleResult = _roleManager.CreateAsync(role).Result;
        }
    }


}
