using MedicalServicesManagement.BLL.Dto;
using MedicalServicesManagement.BLL.Interfaces;
using MedicalServicesManagement.BLL.Jwt;
using MedicalServicesManagement.DAL.Entities;
using MedicalServicesManagement.WebApp.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalServicesManagement.WebApp.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<AuthUser> _identityUserManager;
        private readonly IEntityUserManager _userManager;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(
            UserManager<AuthUser> identityUserManager,
            JwtTokenService jwtTokenService,
            IEntityUserManager userManager)
        {
            _identityUserManager = identityUserManager;
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
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
            ArgumentNullException.ThrowIfNull(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AuthUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpperInvariant(),
                NormalizedEmail = model.Email.ToUpperInvariant(),
                EmailConfirmed = true,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            var creationResult = await _identityUserManager.CreateAsync(user, model.Password);

            if (creationResult.Succeeded)
            {
                await _identityUserManager.AddToRoleAsync(user, Constants.GuestRole);

                var entityUser = new EntityUserDTO()
                {
                    AuthUserId = user.Id,
                    Surname = model.Surname,
                    Name = model.Name,
                    MiddleName = model.MiddleName,
                    BirthDate = model.BirthDate.Value,
                    Telephone = model.Telephone,
                };

                await _userManager.CreateAsync(entityUser);

                // Создаем JWT токен вместо стандартной Identity cookie
                var token = await CreateUserTokenAsync(user);
                SetTokenCookies(token);

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
            ArgumentNullException.ThrowIfNull(model);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _identityUserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            // Проверяем пароль вручную вместо SignInManager
            var passwordValid = await _identityUserManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var token = await CreateUserTokenAsync(user);

            SetTokenCookies(token);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Logout()
        {
            // Удаляем JWT токен из cookies
            SetTokenCookies(jwtToken: null);

            return RedirectToAction("Index", "Home");
        }

        private async Task<string> CreateUserTokenAsync(AuthUser user)
        {
            var roles = await _identityUserManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var entityUser = await _userManager.GetByAuthIdAsync(user.Id);

            return _jwtTokenService.GetToken(user, entityUser, roles, role);
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
