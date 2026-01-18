using Edukate_simulation_AzMPA201.Models;
using Edukate_simulation_AzMPA201.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Edukate_simulation_AzMPA201.Controllers;
public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager) : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);


        AppUser user = new()
        {
            Email = vm.Email,
            Fullname = vm.Fullname,
            UserName = vm.Username,

        };

        var result = await _userManager.CreateAsync(user, vm.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(vm);
        }
        await _userManager.AddToRoleAsync(user, "Member");

        await _signInManager.SignInAsync(user, false);

        return RedirectToAction("Index", "Home");
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("login");
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        var user = await _userManager.FindByEmailAsync(vm.Email);

        if (user is null)
        {
            ModelState.AddModelError("", "Username or password is wrong");
            return View(vm);
        }

        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);

        if (!result.Succeeded)
        {

            ModelState.AddModelError("", "Username or password is wrong");
            return View(vm);
        }


        return RedirectToAction("Index", "Home");
    }


    public async Task<IActionResult> CreateRoles()
    {
        await _roleManager.CreateAsync(new()
        {
            Name = "Member"
        });
        await _roleManager.CreateAsync(new()
        {
            Name = "Admin"
        });

        return Ok("Roles is created");
    }
}
