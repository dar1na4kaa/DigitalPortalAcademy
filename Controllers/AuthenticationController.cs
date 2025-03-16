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
                User newUser = _authenticationService.RegisterUser(email.Trim(), password, role, firstName.Trim(),lastName.Trim(),middleName.Trim(), uniqueNumber);

                if (newUser != null)
                {
                    ViewBag.SuccessMessage = "����������� ������ �������!";
                    return RedirectToAction("Login", "Authentication");
                }

                else
                {
                    ViewBag.ErrorMessage = "������ ��� �����������";
                    return View();
                }
            }

            catch (Exception ex)
            {
                throw new ApplicationException("������ ��� ����������� ������������", ex);
            }
        }
    }
}
