using DigitalPortalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserAccountService _userAccountService;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(UserAccountService userAccountService, AuthenticationService authenticationService)
        {
            _userAccountService = userAccountService;
            _authenticationService = authenticationService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userAccountService.GetUserByEmailAsync(email);

            if (user == null || !_authenticationService.VerifyPassword(user, password))
            {
                ViewBag.ErrorMessage = "Неверный email или пароль!";
                return View();
            }
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Roles.Name);

            return user.Roles.Name switch
            {
                "Администратор" => RedirectToAction("Index", "Administrator"),
                "Преподаватель" => RedirectToAction("UserDashboard", "User"),
                _ => RedirectToAction("AccessDenied", "Home") 
            };
        }

        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string email, string password,string fullName, string uniqueCode, string role)
        {
            try
            {
                var newUser = await _userAccountService.RegisterUserAsync(email, password, role, fullName, uniqueCode);
                if (newUser != null)
                {
                    ViewBag.SuccessMessage = "Регистрация прошла успешно!";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Ошибка при регистрации";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }

        }
    }
}
