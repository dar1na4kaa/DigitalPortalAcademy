using DigitalPortalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("Index action in AdministratorController called");

            return View();
        }
    }
}
