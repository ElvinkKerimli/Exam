using Exam.DAL;
using Exam.Models;
using Exam.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;


namespace Exam.Controllers
{
	public class AccountController : Controller
	{
		AppDbContext _context;
		private readonly UserManager<AppUser> userManager;

		SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			this.userManager = userManager;
			_signInManager = signInManager;
			this.roleManager = roleManager;
		}

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterVm vm)
		{
			
			AppUser appUser = new AppUser();
			{
				appUser.Name = vm.Name;
				appUser.Email = vm.Email;
				appUser.Surname = vm.Surname;
				appUser.UserName = vm.Username;

			}
			
			var result = await userManager.CreateAsync(appUser, vm.Password);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("ConfirmPassword", item.Description);
				}
				return View();
			}
			await userManager.AddToRoleAsync(appUser, "Member");


			return RedirectToAction("Login");
		}
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
		public IActionResult Login()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVm loginVm, string? ReturnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var user = await userManager.FindByEmailAsync(loginVm.EmailOrUsername) ?? await userManager.FindByNameAsync(loginVm.EmailOrUsername);
			if (user == null)
			{
				ModelState.AddModelError("", "Password ve ya username sehvdir ");
				return View();
			}
			var result = await _signInManager.CheckPasswordSignInAsync(user, loginVm.Password, true);
			if (result.IsLockedOut)
			{
				ModelState.AddModelError("", "Birazdan yeniden cehd edin");
				return View();
			}
			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "Password ve ya username sehvdir ");
				return View();
			}
			await _signInManager.SignInAsync(user, loginVm.Reminder);
			if (ReturnUrl != null)
			{
				return Redirect(ReturnUrl);
			}


			return RedirectToAction("Index", "Home");
		}
		public async Task<IActionResult> CreateRole()
		{
			await roleManager.CreateAsync(new IdentityRole()
			{
				Name = "Admin"
			});
			await roleManager.CreateAsync(new IdentityRole()
			{
				Name = "Member"
			});
			return RedirectToAction("Index", "Home");
		}
	}
}