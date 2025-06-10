using Microsoft.AspNetCore.Mvc;
using DigitalPortalAcademy.Services;
using DigitalPortalAcademy.Extensions;
using DigitalPortalAcademy.ViewModels;
using DigitalPortalAcademy.ViewModels.DigitalPortalAcademy.Models.ViewModels;
using DigitalPortalAcademy.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Controllers
{
    public class StudentController : Controller
    {
        [RoleAuthorize("Студент")]

        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Shedule()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0)
                return NotFound();

            var scheduleViewModel = _studentService.GetScheduleForStudent(userId.Value);

            if (scheduleViewModel == null)
                return NotFound();

            return View(scheduleViewModel);
        }
        public IActionResult Curriculum()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0)
                return NotFound();

            var curriculum = _studentService.GetCurriculumForStudent(userId.Value);
            if (curriculum == null)
                return NotFound();

            string planFileUrl = Url.Content(curriculum.PlanFilePath);

            return View(model: planFileUrl);
        }
        [HttpGet]
        public IActionResult Certificate()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0)
                return NotFound();

            var model = _studentService.GetReferenceRequests(userId.Value);
            return View(model);
        }

        [HttpPost]
        public IActionResult Certificate(string referenceType, string? comment)
        {
            var userId = HttpContext.GetCurrentUserId();

            if (userId is null or 0 || string.IsNullOrWhiteSpace(referenceType))
                return BadRequest("Неверные данные для запроса справки.");

            _studentService.CreateReferenceRequest(userId.Value, referenceType.Trim(), comment?.Trim());

            TempData["SuccessMessage"] = "Запрос на справку успешно отправлен!";

            return RedirectToAction(nameof(Certificate));
        }

        public IActionResult Grades(string month)
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null)
                return NotFound();

            var model = new PerformanceViewModel();

            model.SelectedMonth = month ?? "все";

            model.MonthsSelectList = GetMonthsSelectList(model.SelectedMonth);

            model.PerformanceItems = _studentService.GetPerformance(userId.Value, model.SelectedMonth);
            model.HasDebt = model.PerformanceItems.Any(p => p.Mark == 2);

            
            return View(model);
        }

        private List<SelectListItem> GetMonthsSelectList(string selectedMonth)
        {
            var months = new[]
            {
        "все", "сентябрь", "октябрь", "ноябрь", "декабрь", "январь", "февраль",
        "март", "апрель", "май", "июнь"
    };

            return months.Select(m => new SelectListItem
            {
                Text = char.ToUpper(m[0]) + m.Substring(1),
                Value = m,
                Selected = m == selectedMonth
            }).ToList();
        }

        [HttpGet]
        public IActionResult Account()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0) return NotFound();

            var viewModel = _studentService.GetUserById(userId.Value);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditAccount()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0) return NotFound();

            var viewModel = _studentService.GetAccountForEdit(userId.Value);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult EditAccount(EditAccountInformationViewModel model)
        {
            bool success = _studentService.UpdateAccount(model);
            if (!success)
            {
                TempData["Error"] = "Произошла ошибка при добавлении. Повторите ошибку позже";
                return RedirectToAction("EditAccount", "Student");
            }

            else
            {
                TempData["Success"] = "Аккаунт успешно изменен";
                return RedirectToAction("EditAccount", "Student");
            }
        }
        [HttpGet]
        public IActionResult Announcements()
        {
            return View(_studentService.GetNews());
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
