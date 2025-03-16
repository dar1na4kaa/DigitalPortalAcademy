using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.Services;
using DigitalPortalAcademy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DigitalPortalAcademy.Controllers
{
    [RoleAuthorize("Администратор")]
    public class AdministratorController : Controller
    {
        private readonly AdministratorService _adminService;
        public AdministratorController(AdministratorService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult Dashboard()
        {
            return View(_adminService.GetUsers());
        }
        public IActionResult Details(int id)
        {
            var user = _adminService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _adminService.GetUserForEdit(id);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            bool success = _adminService.UpdateUser(model);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction("Dashboard", "Administrator");
        }

        public IActionResult Delete(int id)
        {
            var user = _adminService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _adminService.DeleteUser(id);
            return RedirectToAction("Dashboard");
        }
    }
}
