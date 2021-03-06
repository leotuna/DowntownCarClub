using DowntownCarClub.Web.Dtos;
using DowntownCarClub.Web.Models;
using DowntownCarClub.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DowntownCarClub.Web.Controllers
{
    [Route("/auth")]
    public class AuthController : Controller
    {
        private SignInManager<User> SignInManager { get; }
        private UserManager<User> UserManager { get; }
        private EmailService EmailService { get; }

        public AuthController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            EmailService emailService)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            EmailService = emailService;
        }

        [HttpGet("sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            var result = await SignInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Index");
            }

            return View();
        }

        [HttpGet("sign-up")]
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpDto model, [FromQuery] string redirect)
        {
            var user = new User { UserName = model.UserName, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false);
                if (!string.IsNullOrWhiteSpace(redirect))
                {
                    return Redirect(redirect);
                }
                return RedirectToAction("Index", "Index");
            }
            throw new NotImplementedException();
        }

        [HttpGet("log-out")]
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #region to do
        //[HttpGet("recover-password")]
        //public IActionResult RecoverPassword()
        //{
        //    return View();
        //}

        //[HttpPost("recover-password")]
        //public async Task<IActionResult> RecoverPassword(RecoverPasswordDto model)
        //{
        //    var user = await UserManager.FindByNameAsync(model.Email);

        //    if (user is null)
        //    {
        //        return View();
        //    }

        //    var code = await UserManager.GeneratePasswordResetTokenAsync(user);

        //    try
        //    {
        //        EmailService.SendPasswordRecovery(code, user.Email);
        //    }
        //    catch
        //    {
        //        throw new NotImplementedException();
        //    }

        //    return View();
        //}

        //[HttpGet("recover-password/confirm")]
        //public IActionResult ConfirmPasswordRecovery([FromQuery] string id, [FromQuery] string email)
        //{
        //    return View(new ConfirmPasswordRecoveryDto { Id = id, Email = email });
        //}

        //[HttpPost("recover-password/confirm")]
        //public async Task<IActionResult> ConfirmPasswordRecovery([FromQuery] string id, [FromQuery] string email, ConfirmPasswordRecoveryDto model)
        //{
        //    var user = await UserManager.FindByNameAsync(email);

        //    var result = await UserManager.ResetPasswordAsync(user, id, model.Password);

        //    if (!result.Succeeded)
        //    {
        //        return RedirectToAction(nameof(RecoverPassword));
        //    }

        //    return RedirectToAction(nameof(SignIn));
        //}
        #endregion
    }
}
