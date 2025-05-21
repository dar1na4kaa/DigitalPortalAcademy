using DigitalPortalAcademy.Services;
using DigitalPortalAcademy.Extensions;
using DigitalPortalAcademy.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            return View(_adminService.GetUsers(HttpContext.GetCurrentUserId()));
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
        [HttpGet]
        public IActionResult Account()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0) return NotFound();

            var viewModel = _adminService.GetUserById(userId.Value);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditAccount()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0) NotFound();

            var viewModel = _adminService.GetAccountForEdit(userId.Value);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult EditAccount(EditAccountInformationViewModel model)
        {
            bool success = _adminService.UpdateAccount(model);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction("Dashboard", "Administrator");
        }
    }
}
