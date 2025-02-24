using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        public AdministratorController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            var userId = HttpContext.Session.GetString("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (HttpContext.Session.GetString("UserId") == null && HttpContext.Session.GetString("UserRole").Equals("Администратор"))
            {
                RedirectToAction("Login", "Authentication");
            }
        }

        public IActionResult Dashboard()
        {
            Console.WriteLine("Index action in AdministratorController called");

            return View();
        }
    }
}
