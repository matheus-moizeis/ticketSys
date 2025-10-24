using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSys.Application.DTOs;
using TicketSys.Domain.Account;
using TicketSys.WebUI.ViewModels;

namespace TicketSys.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController(IAuthenticate authenticate) : Controller
    {
        private readonly IAuthenticate _authenticate = authenticate;

        #region Login

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginDTO()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View("Login", login);
            }

            var result = await _authenticate.Authenticate(login.Email!, login.Password!);

            if (result)
            {
                if (!string.IsNullOrEmpty(login.ReturnUrl))
                {
                    return Redirect(login.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "OrderService");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login inválido");
                return View("Login", login);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _authenticate.Logout();
            // Redirect to Login (Post/Redirect/Get) to avoid rendering the login view without proper URL/model
            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #endregion

        #region Users

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _authenticate.GetAllAsync();
            var roles = await _authenticate.GetAllRolesAsync();

            var model = users.Select(u => new AccountViewModel
            {
                Id = u.Id,
                Email = u.Email ?? string.Empty,
                Name = u.Name ?? string.Empty,
                IsActive = u.IsActive
            });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var roles = await _authenticate.GetAllRolesAsync();
            var model = new AccountViewModel
            {
                Id = string.Empty,
                Email = string.Empty,
                Name = string.Empty,
                Password = string.Empty,
                IsActive = true,
                TypesOfAccount = roles.Select(r => new TypeofAccountViewModel(
                    r.Id,
                    r.Description
                )),
                SelectedTypeOfAccountId = string.Empty
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(userVM);
            }

            var result = await _authenticate.RegisterUser(userVM.Email!, userVM.Password!, userVM.Name!, userVM.SelectedTypeOfAccountId!);

            if (result)
            {
                return RedirectToAction("Users", "Account");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registro inválido");
                return View(userVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _authenticate.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _authenticate.GetAllRolesAsync();

            var model = new AccountViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                IsActive = user.IsActive,
                TypesOfAccount = roles.Select(r => new TypeofAccountViewModel(r.Id, r.Description)),
                SelectedTypeOfAccountId = user.TypeOfAccountId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountViewModel userVM)
        {
            var result = await _authenticate.UpdateUser(
                userVM.Id!,
                userVM.Email!,
                userVM.Password!,
                userVM.Name!,
                userVM.IsActive,
                userVM.SelectedTypeOfAccountId!
            );

            if (result)
            {
                return RedirectToAction("Users", "Account");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Atualização inválida");
                return View(userVM);
            }
        }
        #endregion

    }
}
