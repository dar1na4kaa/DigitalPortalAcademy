using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly RegistrationService _registrationService;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(RegistrationService registrationService, AuthenticationService authenticationService)
        {
            _registrationService = registrationService;
            _authenticationService = authenticationService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _authenticationService.GetUserByEmailAsync(email);

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

            return await RolesService.GetActionByRole(this, user);
        }


        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(string email, string password, string fullName, string uniqueCode, string role)
        {

            try
            {
                var newUser = await _registrationService.RegisterUserAsync(email, password, role, fullName, uniqueCode);
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
