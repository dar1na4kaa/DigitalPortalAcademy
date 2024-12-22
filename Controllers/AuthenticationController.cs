using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserAccountService _userAccountService;
        private readonly Services.AuthenticationService _authenticationService;

        public AuthenticationController(UserAccountService userAccountService, Services.AuthenticationService authenticationService)
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
                ViewBag.ErrorMessage = "�������� email ��� ������!";
                return View();
            }
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role); // ��������������, ��� � ������������ ���� �������� Role

            // �������������� � ����������� �� ����
            return user.Role switch
            {
                "�������������" => RedirectToAction("Index", "Administrator"),
                "�������������" => RedirectToAction("UserDashboard", "User"),
                _ => RedirectToAction("AccessDenied", "Home") 
            };
        }

        public IActionResult Logout()
        {
            return View();
        }
        /*        [HttpPost]
        */
        [HttpPost]
        public async Task<IActionResult> Logout(string email, string password, string confirmPassword, string fullName, string uniqueCode, string role)
        {
            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "������ �� ���������!";
                return View();
            }

            try
            {
                var newUser = await _userAccountService.RegisterUserAsync(email, password, role, fullName, uniqueCode);
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

    }
}
