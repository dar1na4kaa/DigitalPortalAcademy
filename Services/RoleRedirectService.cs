using DigitalPortalAcademy.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Services
{
    public static class RoleRedirectService
    {
        public static IActionResult GetActionByRole(Controller controller, User user)
        {
            if (controller == null || user?.Role?.Name == null)
            {
                return controller.RedirectToAction("Login", "Authentication");
            }

            return user.Role.Name switch
            {
                "Администратор" => controller.RedirectToAction("Dashboard", "Administrator"),
                "Студент" => controller.RedirectToAction("Dashboard", "Student"),
                "Сотрудник учебной части" => controller.RedirectToAction("Dashboard", "Department"),
                "Преподаватель" => controller.RedirectToAction("Dashboard", "Teacher"),
                null or _ => controller.RedirectToAction("Login", "Authentication")
            };
        }
    }
}
