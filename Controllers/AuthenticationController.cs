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
                ViewBag.ErrorMessage = "�������� email ��� ������!";
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
        public IActionResult SignUp(string email, string password, string role, string firstName, string lastName, string middleName, string uniqueNumber)
        {
            try
            {
                User newUser = _authenticationService.RegisterUser(email, password, role, firstName,lastName,middleName, uniqueNumber);

                if (newUser != null)
                {
                    ViewBag.SuccessMessage = "����������� ������ �������!";
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "������ ��� �����������";
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
