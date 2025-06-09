using DigitalPortalAcademy.Extensions;
using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.Services;
using DigitalPortalAcademy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Controllers
{
    public class DepartmentController : Controller
    {
        [RoleAuthorize("Сотрудник учебной части")]

        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Curriculum()
        {
            var specialties = _departmentService.GetAllSpecialties();
            ViewBag.Specialties = specialties;
            return View();
        }

        [HttpPost]
        public IActionResult Curriculum(CurriculumAddViewModel model)
        {
            try
            {
                _departmentService.SaveCurriculum(model);
                TempData["Message"] = "Учебный план успешно добавлен.";
                TempData["MessageType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Ошибка при добавлении учебного плана: " + ex.Message;
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("Curriculum");
        }
        [HttpGet]
        public IActionResult Annoucements()
        {
            return View(_departmentService.GetAnnouncements());
        }

        [HttpPost]
        public IActionResult Annoucements(AnnouncementAddViewModel model)
        {
            var userId = HttpContext.GetCurrentUserId();

            try
            {
                _departmentService.SaveAnnoucement(model, userId.Value);
                TempData["Success"] = "Объявление успешно добавлено";
                return RedirectToAction("Annoucements");
            }

            catch
            {
                TempData["Error"] = "Произошла ошибка при добавлении. Повторите ошибку позже";
                return RedirectToAction("Annoucements");
            }
        }

        [HttpGet]
        public IActionResult Account()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0) return NotFound();

            var viewModel = _departmentService.GetUserById(userId.Value);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult EditAccount()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0) return NotFound();

            var viewModel = _departmentService.GetAccountForEdit(userId.Value);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult EditAccount(EditAccountInformationViewModel model)
        {
            bool success = _departmentService.UpdateAccount(model);
            if (!success)
            {
                TempData["Error"] = "Произошла ошибка при добавлении. Повторите ошибку позже";
                return RedirectToAction("EditAccount", "Department");
            }
            else
            {
                TempData["Success"] = "Аккаунт успешно изменен";
                return RedirectToAction("EditAccount", "Department");
            }
        }
    }
}
