using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LiBook.Data.Entities;
using LiBook.Models;
using LiBook.Models.Account;
using LiBook.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LiBook.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<UserProfile> _userManager;
        private readonly SignInManager<UserProfile> _signInManager;
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<UserProfile> userManager,
            SignInManager<UserProfile> signInManager,
            IUserService userService,
            ILogger<AccountController> logger,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        #region Profile

        [Authorize]
        public IActionResult Index()
        {
            try
            {
                var user = _userService.GetUserProfile(User);
                var model = _mapper.Map<UserProfile, UserProfileViewModel>(user);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        [Authorize]
        public IActionResult Edit()
        {
            try
            {
                var user = _userService.GetUserProfile(User);
                var model = _mapper.Map<UserProfile, UserProfileViewModel>(user);
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        [Authorize]
        public IActionResult EditConfirmed([Bind("Id,FirstName,LastName")]UserProfileViewModel profile)
        {
            try
            {
                var model = _mapper.Map<UserProfileViewModel, UserProfile>(profile);
                _userService.Update(model);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }

        }

        [Authorize]
        public IActionResult Comments()
        {
            try
            {
                var user = _userService.GetUserProfile(User);
                var model = _mapper.Map<UserProfile, UserProfileViewModel>(user);
                return View(model.Comments.AsEnumerable());
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }

        }

        #endregion

        #region Login

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            try
            {
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        return RedirectToLocal(returnUrl);
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
                catch (Exception e)
                {
                    return View("Error", new ErrorViewModel
                    {
                        RequestId = Request.HttpContext.TraceIdentifier,
                        Exception = e
                    });
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region Registration

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new UserProfile { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        // var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                        // await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                        // await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        _logger.LogInformation("User created a new account with password.");
                        return RedirectToLocal(returnUrl);
                    }
                    AddErrors(result);
                }
                catch (Exception e)
                {
                    return View("Error", new ErrorViewModel
                    {
                        RequestId = Request.HttpContext.TraceIdentifier,
                        Exception = e
                    });
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = Request.HttpContext.TraceIdentifier,
                    Exception = e
                });
            }

        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}