using Microsoft.AspNetCore.Identity;
using TicketSys.Domain.Account;

namespace TicketSys.Infra.Data.Identity;

public class AuthenticateService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthenticate
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

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

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> RegisterUser(string email, string password)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
            await _signInManager.SignInAsync(user, isPersistent: false);

        return result.Succeeded;
    }
}
