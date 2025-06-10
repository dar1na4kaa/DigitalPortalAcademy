using DigitalPortalAcademy.Services;
using DigitalPortalAcademy.Extensions;
using DigitalPortalAcademy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DigitalPortalAcademy.Models;

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
                TempData["Error"] = "Произошла ошибка при добавлении. Повторите ошибку позже";
                return RedirectToAction("Edit", "Administrator");
            }
            else
            {
                TempData["Success"] = "Пользователь успешно изменен";
                return RedirectToAction("Edit", "Administrator");
            }
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
            if (userId == null || userId == 0) return NotFound();

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
                TempData["Error"] = "Произошла ошибка при добавлении. Повторите ошибку позже";
                return RedirectToAction("EditAccount", "Administrator");
            }

            else
            {
                TempData["Success"] = "Аккаунт успешно изменен";
                return RedirectToAction("EditAccount", "Administrator");

            }
        }
        public IActionResult Index(int? roleId, string? search)
        {
            ViewBag.CurrentRoleId = roleId;
            ViewBag.CurrentSearch = search;

            var users = _adminService.GetFilteredUsers(roleId, search);
            return View(users);
        }
        [HttpGet]
        public IActionResult Announcements()
        {
            return View(_adminService.GetAnnouncements());
        }

        [HttpPost]
        public IActionResult Annoucements(AnnouncementAddViewModel model)
        {
            var userId = HttpContext.GetCurrentUserId();

            try
            {
                _adminService.SaveAnnoucement(model, userId.Value);
                TempData["Success"] = "Объявление успешно добавлено";
                return RedirectToAction("Annoucements");
            }

            catch
            {
                TempData["Error"] = "Произошла ошибка при добавлении. Повторите ошибку позже";
                return RedirectToAction("Annoucements");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Authentication");
        }
    }
}
