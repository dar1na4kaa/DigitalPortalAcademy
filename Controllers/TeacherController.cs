using DigitalPortalAcademy.Extensions;
using DigitalPortalAcademy.Services;
using DigitalPortalAcademy.Models;
using DigitalPortalAcademy.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Controllers
{
    [RoleAuthorize("Преподаватель")]
    public class TeacherController : Controller
    {
        private readonly TeacherService _teacherService;

        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Group()
        {
            var teacherId = HttpContext.GetCurrentUserId();
            if (teacherId == null || teacherId == 0) return NotFound();
            var groups = _teacherService.GetTeacherGroups(teacherId.Value);
            return View(groups);
        }

        public IActionResult GroupDetails(int id)
        {
            var group = _teacherService.GetGroupDetails(id);
            return View(group);
        }

        [HttpGet]
        public IActionResult Report()
        {
            var teacherId = HttpContext.GetCurrentUserId();
            if (teacherId == null || teacherId == 0) return NotFound();
            var model = _teacherService.GetInitialReportModel(teacherId.Value);
            ViewBag.Reports = _teacherService.GetReportsByTeacher(teacherId.Value);
            return View(model);
        }

        [HttpPost]
        public IActionResult Report(ReportFilterViewModel model)
        {
            var teacherId = HttpContext.GetCurrentUserId();
            if (teacherId == null || teacherId == 0) return NotFound();
            var updatedModel = _teacherService.GetReportData(
                teacherId.Value, model.SelectedGroupId, model.SelectedSubjectId
            );

            if (!updatedModel.Subjects.Any(s => s.SubjectId == model.SelectedSubjectId))
            {
                ViewBag.SubjectError = "Выбранный предмет не преподаётся в этой группе.";
                return View(updatedModel);
            }

            return View(updatedModel);
        }

        [HttpPost]
        public IActionResult SaveReport(ReportFilterViewModel model)
        {
            var teacherId = HttpContext.GetCurrentUserId();
            if (teacherId == null || teacherId == 0) return NotFound();

            if (!ModelState.IsValid)
            {
                var updatedModel = _teacherService.GetReportData(
                    teacherId.Value, model.SelectedGroupId, model.SelectedSubjectId
                );
                return View("Report", updatedModel);
            }

            _teacherService.SaveReport(teacherId.Value, model);
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        public IActionResult ReportDetail(int id)
        {
            var report = _teacherService.GetReportWithDetails(id);
            if (report == null)
                return NotFound();

            return View(report);
        }
        [HttpPost]
        public IActionResult DeleteReport(int id)
        {
            _teacherService.DeleteReport(id);
            return RedirectToAction("Report");
        }
        [HttpGet]
        public IActionResult Account()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0) return NotFound();

            var viewModel = _teacherService.GetUserById(userId.Value);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult EditAccount()
        {
            var userId = HttpContext.GetCurrentUserId();
            if (userId == null || userId == 0) return NotFound();

            var viewModel = _teacherService.GetAccountForEdit(userId.Value);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult EditAccount(EditAccountInformationViewModel model)
        {
            bool success = _teacherService.UpdateAccount(model);
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
        [HttpGet]
        public IActionResult Announcements()
        {
            return View(_teacherService.GetNews());
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
