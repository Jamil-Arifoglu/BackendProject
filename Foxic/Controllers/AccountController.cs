using Foxic.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Foxic.ViewModels;
using System.Data;
using NuGet.Protocol.Plugins;

namespace Foxic.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM account)
		{
			if (!ModelState.IsValid) return View();
			if (!account.Terms) return View();
			User user = new User
			{
				UserName = account.Username,
				Fullname = string.Concat(account.Firstname, " ", account.Lastname),
				Email = account.Email
			};
			IdentityResult result = await _userManager.CreateAsync(user, account.Password);
			if (!result.Succeeded)
			{
				foreach (IdentityError message in result.Errors)
				{
					ModelState.AddModelError("", message.Description);
				}
				return View();
			}

			return RedirectToAction("Index", "Foxic");

		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM login)
		{
			if (!ModelState.IsValid) return View();

			User user = await _userManager.FindByNameAsync(login.Username);
			//IList<string> roles = await _userManager.GetRolesAsync(user);

			if (user is null)
			{
				ModelState.AddModelError("", "Username or password is incorrect");
				return View();
			}
			Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
			if (!result.Succeeded)
			{
				if (result.IsLockedOut)
				{
					ModelState.AddModelError("", "Due to overtyring your account has been blocked for 5 minutes");
					return View();
				}
				ModelState.AddModelError("", "Username or password is incorrect");
				return View();
			}

			return RedirectToAction("Index", "Foxic");

		}

		public async Task<IActionResult> Detail()
		{
			User user = await _userManager.FindByNameAsync(User.Identity.Name);
			UserVM users = new()
			{
				Email = user.Email,
				UserName = user.UserName

			};

			return View(users);
		}
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Foxic");
		}

		public IActionResult ShowAuthenticated()
		{
			return Json(User.Identity.IsAuthenticated);
		}

	}
}

