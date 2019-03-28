using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LiBook.Identity
{
    public class AuthController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;

        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([Bind("Email, Password")] SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                return Redirect("/");
            }

            return Redirect("/Home/Error");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync().Wait();
            return Redirect("/Home");
        }
    }
}