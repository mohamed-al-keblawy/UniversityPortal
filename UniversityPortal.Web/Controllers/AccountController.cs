using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversityPortal.DAL.Interfaces;
using UniversityPortal.Models;
using UniversityPortal.Models.Entities;
using UniversityPortal.Web.ViewModels;

namespace UniversityPortal.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userRepository.GetByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            string roleName = user.RoleId switch
            {
                1 => "Admin",
                2 => "Faculty",
                3 => "Student",
                _ => "Unknown"
            };

            // Create claims
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roleName), // For [Authorize(Roles = "Faculty")] etc.
            new Claim("RoleId", user.RoleId.ToString())
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Role-based redirect
            return user.RoleId switch
            {
                1 => RedirectToAction("Index", "Admin"),
                2 => RedirectToAction("Index", "Assignments"), // Faculty
                3 => RedirectToAction("Dashboard", "Student"), // Student
                _ => RedirectToAction("Login")
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check if email is already used (optional)
            var existing = await _userRepository.GetByEmailAsync(model.Email);
            if (existing != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }

            // Hash the password securely
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                RoleId = model.RoleId
            };
            user.PasswordHash = hasher.HashPassword(user, model.Password);

            await _userRepository.RegisterAsync(user);

            return RedirectToAction("Login");
        }

    }
}
