using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolarPanelInstallationManagement.Models.DTOs.Account;
using SolarPanelInstallationManagement.Models.Entities;

namespace SolarPanelInstallationManagement.Controllers
{

    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // --------------------
        // GET: /Account/Login
        // --------------------
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // --------------------
        // POST: /Account/Login
        // --------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model,
            string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "ConsumerSurvey");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Account is locked. Please try again later.");
            }
            else
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid username or password.");
            }

            return View(model);
        }

        // --------------------
        // POST: /Account/Logout
        // --------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // --------------------
        // GET: /Account/AccessDenied
        // --------------------
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
