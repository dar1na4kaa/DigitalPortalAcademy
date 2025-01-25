using DigitalPortalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Controllers
{
    public class AdministratorController : Controller
    {
        public AdministratorController(RegistrationService registrationService, AuthenticationService authenticationService)
        {
        }
        public IActionResult Index()
        {
            Console.WriteLine("Index action in AdministratorController called");

            return View(); // Это будет искать файл /Views/Administrator/Index.cshtml
        }
    }
}
