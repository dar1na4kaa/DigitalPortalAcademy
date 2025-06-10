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
            int userId = HttpContext.GetCurrentUserId().Value;
            var groups = _teacherService.GetTeacherGroups(userId);
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
            int teacherId = HttpContext.GetCurrentUserId().Value;
            var model = _teacherService.GetInitialReportModel(teacherId);
            ViewBag.Reports = _teacherService.GetReportsByTeacher(teacherId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Report(ReportFilterViewModel model)
        {
            int teacherId = HttpContext.GetCurrentUserId().Value;

            var updatedModel = _teacherService.GetReportData(
                teacherId, model.SelectedGroupId, model.SelectedSubjectId
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
            int teacherId = HttpContext.GetCurrentUserId().Value;

            if (!ModelState.IsValid)
            {
                var updatedModel = _teacherService.GetReportData(
                    teacherId, model.SelectedGroupId, model.SelectedSubjectId
                );
                return View("Report", updatedModel);
            }

            _teacherService.SaveReport(teacherId, model);
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
    }
}
