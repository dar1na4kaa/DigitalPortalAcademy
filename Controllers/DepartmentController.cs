using DigitalPortalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Controllers
{
    public class DepartmentController: Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Curriculum() { 
            return View();  
        }
    }
}
