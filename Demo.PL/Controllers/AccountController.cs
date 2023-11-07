using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		//Register
		//Base Url/Account/Register
		public AccountController(UserManager<ApplicationUser> userManager
			,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)//Server Side Validation
			{
				var User = new ApplicationUser()
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					Fname=model.FName,
					Lname=model.LName,
					IsAgree=model.IsAgree,

				};
               
				var Result=await _userManager.CreateAsync(User,model.Password);
				if(Result.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}
				else
				{
					foreach(var error in Result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
						
					}
				}
				
			}
			return View(model);
		}

		//Login

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if(User is not null)
				{
					//Login
					var Flag= await _userManager.CheckPasswordAsync(User, model.Password);
					if (Flag)
					{
					 var Result=	await _signInManager.PasswordSignInAsync(User,model.Password, model.RememberMe,false);
						if (Result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Incorrect Password");
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email Is Not Exists");
				}
			}
			return View(model);
		}
		//Sign Out

		public new async Task<IActionResult> SignOut()//new=>To make It Hide From The Inhereted Method SignOut At Class Controllor
		{
		  await _signInManager.SignOutAsync();//Used To Delete My Token
			return RedirectToAction(nameof(Login));
		}

		//Forget Password

		public IActionResult ForgetPassword()
		{
			return View();
		}

		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
                    //Send Email
                    var token =await _userManager.GeneratePasswordResetTokenAsync(User);//Valid For Only One Time For THis User
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new {email=User.Email,Token=token},Request.Scheme/*"https","localhost:44311"*/);
					var Email = new Email()
					{
						Subject = "Reset Password",
						To = model.Email,
						Body = ResetPasswordLink
                    };
					EmailSettings.SendEmail(Email);
					return RedirectToAction(nameof(CheckYourInbox));

				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email Is Not Exists");
				}

			}
			
			return View("ForgetPassword ",model);
			
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}
		//Reset Password

		public IActionResult ResetPassword(string email,string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				string Email = TempData["email"] as string;
				string token = TempData["token"] as string;
				var User= await _userManager.FindByEmailAsync(Email);//Used To Find The User OF This Email
				var Result= await _userManager.ResetPasswordAsync(User, token,model.NewPassword);
				if (Result.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}
				else
				{
					foreach (var error in Result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}


	}
}
