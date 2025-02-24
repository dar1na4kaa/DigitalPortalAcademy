using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _authenticationService.GetUserByEmail(email);

            if (user == null || !_authenticationService.VerifyPassword(user, password))
            {
                ViewBag.ErrorMessage = "Неверный email или пароль!";
                return View();
            }

            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("UserEmail", user.Login);
            HttpContext.Session.SetString("UserRole", user.Role.Name);

            Console.WriteLine("UserId: " + HttpContext.Session.GetString("UserId"));
            Console.WriteLine("UserEmail: " + HttpContext.Session.GetString("UserEmail"));
            Console.WriteLine("UserRole: " + HttpContext.Session.GetString("UserRole"));

            return RoleRedirectService.GetActionByRole(this, user);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string email, string password, string fullName, string uniqueCode, string role)
        {
            try
            {
                var newUser = _authenticationService.RegisterUser(email, password, role, fullName, uniqueCode); // Синхронный вызов

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
