using Foxic.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Foxic.ViewModels;
using System.Data;
using NuGet.Protocol.Plugins;
using Foxic.Utilities.Roles;

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
			await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
			return RedirectToAction("Index", "Foxic");

		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM login)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var user = await _userManager.FindByNameAsync(login.Username);
			if (user == null)
			{
				ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
				return View();
			}

			var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Foxic");
			}

			if (result.IsLockedOut)
			{
				ModelState.AddModelError("", "Hesabınız, 5 dakika boyunca kitlenmiştir");
				return View();
			}

			ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
			return View();
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
		//public async Task CreateRoles()
		//{
		//	await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
		//	await _roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
		//	await _roleManager.CreateAsync(new IdentityRole(Roles.Member.ToString()));
		//}

	}
}

