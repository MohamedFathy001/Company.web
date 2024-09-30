using Company.Data.Entities;
using Company.Service.Helper;
using Company.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _UserManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager  , SignInManager<ApplicationUser> signInManager) 
        {
            _UserManager = userManager;
			_signInManager = signInManager;
		}


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = input.Email.Split("@")[0],
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    IsActive = true
                };
                var result = await _UserManager.CreateAsync(user , input.Password);

                if (result.Succeeded) 
                { 
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors) 
                    {
                        ModelState.AddModelError("" , item.Description);
                    }
                }

            }
            return View(input);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(input.Email);

                if(user is not null)
                {
                    if(await _UserManager.CheckPasswordAsync(user , input.Password))
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, true);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
					}
                }
                else
                {
                    ModelState.AddModelError("", " Incorrect Email or Password");
                }
            }
            return View(input);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
		{
            if(ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(input.Email);
                if(user is not null)
                {
                    var token =await _UserManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword" , "Account" , new {Email = input.Email , Token= token});
                    var email = new Email { 
                        body = url,
                        subject = "Reset Password",
                        To = input.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
			return View(input);
		}

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public IActionResult ResetPassword(string Email , string Token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(input.Email);

                if(user is not null)
                {
                    var result = await _UserManager.ResetPasswordAsync(user , input.Token , input.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                }
            }
            return View(input);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
