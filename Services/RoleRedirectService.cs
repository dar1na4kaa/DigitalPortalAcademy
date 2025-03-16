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
                return controller.RedirectToAction("AccessDenied", "Home");
            }

            return user.Role.Name switch
            {
                "Администратор" => controller.RedirectToAction("Dashboard", "Administrator"),
                "Преподаватель" => controller.RedirectToAction("Login", "Authentication"),
                _ => throw new NotImplementedException()
            };
        }
    }
}
