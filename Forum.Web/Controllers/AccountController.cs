using Forum.Application.Exceptions.Models;
using Forum.Application.Features.AccountFeatures.Commands.Registration;
using Forum.Application.Features.AccountFeatures.Commands.ResetPassword.NewPassword;
using Forum.Application.Features.AccountFeatures.Commands.ResetPassword.SendOtp;
using Forum.Application.Features.AccountFeatures.Commands.ResetPassword.Validate;
using Forum.Application.Features.AccountFeatures.Queries.Login;
using Forum.Application.Features.AccountFeatures.Queries.Refresh;
using Forum.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _mediator.Send(new ForgotPasswordCommand { Email = model.Email });

            HttpContext.Session.SetString("ResetEmail", model.Email);
            HttpContext.Session.SetString("ResetGeneratedAt", DateTime.Now.ToString("O"));

            TempData["Message"] = "OTP has been sent to your email.";
            return RedirectToAction("ValidateOtp");
        }

        [HttpGet]
        public IActionResult ValidateOtp()
        {
            return View(new ValidateOtpViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ValidateOtp(ValidateOtpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }            

            var email = HttpContext.Session.GetString("ResetEmail");
            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Your session expired. Please request OTP again.";
                return RedirectToAction("ForgotPassword");
            }

            try
            {
                var resetToken = await _mediator.Send(new ValidateOtpCommand
                {
                    Email = email,
                    Otp = model.Otp
                });

                HttpContext.Session.SetString("ResetToken", resetToken!);
                HttpContext.Session.SetString("ResetGeneratedAt", DateTime.UtcNow.ToString("O"));

                return RedirectToAction("ResetPassword");
            }
            catch (AppException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
               
            var email = HttpContext.Session.GetString("ResetEmail");
            var token = HttpContext.Session.GetString("ResetToken");

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Your session expired. Please restart the reset process.";
                return RedirectToAction("ForgotPassword");
            }

            await _mediator.Send(new ResetPasswordCommand
            { 
                Email = email,
                ResetToken = token,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword         
            });

            HttpContext.Session.Remove("ResetEmail");
            HttpContext.Session.Remove("ResetToken");
            HttpContext.Session.Remove("ResetGeneratedAt");

            TempData["Message"] = "Your password has been successfully reset. Please login.";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet()]
        public IActionResult Login()
        {
            if (Request.Cookies.ContainsKey("jwt"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginViewModel());
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
                
            try
            {
                var token = await _mediator.Send(new LoginQuery(model.Username, model.Password));

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(7)
                };

                HttpContext.Response.Cookies.Append("jwt", token.Token, cookieOptions);
                HttpContext.Response.Cookies.Append("refreshToken", token.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                    Expires = DateTime.Now.AddDays(7)
                });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var command = new RegisterUserCommand
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password
                };

                await _mediator.Send(command);

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            if (Request.Cookies.ContainsKey("jwt"))
            {
                HttpContext.Response.Cookies.Delete("jwt");
                HttpContext.Response.Cookies.Delete("refreshToken");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var accessToken = Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var newToken = await _mediator.Send(new RefreshTokenCommand 
                { 
                    AccessToken = accessToken, 
                    RefreshToken = refreshToken
                });

                HttpContext.Response.Cookies.Append("jwt", newToken.Token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                    Expires = DateTime.Now.AddMinutes(7)
                });

                HttpContext.Response.Cookies.Append("refreshToken", newToken.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.Now.AddDays(7)
                });

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                HttpContext.Response.Cookies.Delete("jwt");
                HttpContext.Response.Cookies.Delete("refreshToken");
                return RedirectToAction("Login", "Account");
            }
        }

    }
}
