using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.WebApp.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServicesManagement.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(
            SignInManager<AuthUser> signInManager,
            UserManager<AuthUser> userManager,
            JwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AuthUser
            {
                Id = Guid.NewGuid().ToString(),
                Surname = model.Surname,
                Name = model.Name,
                MiddleName = model.MiddleName,
                BirthDate = model.BirthDate,
                Telephone = model.Telephone,
                Email = model.Email,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                NormalizedEmail = model.Email.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var creationResult = await _userManager.CreateAsync(user, model.Password);

            if (creationResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Constants.PatientRole);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in creationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            var token = await CreateUserTokenAsync(user);

            SetTokenCookies(token);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            SetTokenCookies(jwtToken: null);

            return RedirectToAction("Index", "Home");
        }

        private async Task<string> CreateUserTokenAsync(AuthUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            return _jwtTokenService.GetToken(user, roles, role);
        }

        private void SetTokenCookies(string jwtToken)
        {
            if (jwtToken != null)
            {
                HttpContext.Response.Cookies.Append(Constants.JwtCookiesKey, jwtToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict,
                    });
            }
            else
            {
                HttpContext.Response.Cookies.Delete(Constants.JwtCookiesKey);
            }
        }
    }
}
