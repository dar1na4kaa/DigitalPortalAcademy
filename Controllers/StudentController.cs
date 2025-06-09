using Microsoft.AspNetCore.Mvc;
using DigitalPortalAcademy.Services;
using DigitalPortalAcademy.Extensions;
using DigitalPortalAcademy.ViewModels;

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

    }
}
