using DigitalPortalAcademy.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPortalAcademy.Services
{
    public static class RolesService
    {
        public static Task<IActionResult> GetActionByRole(ControllerBase controller, User user)
        {
            if (controller == null || user?.Role?.Name == null)
            {
                return Task.FromResult<IActionResult>(controller.RedirectToAction("AccessDenied", "Home"));
            }

            return user.Role.Name switch
            {
                "Администратор" => Task.FromResult<IActionResult>(controller.RedirectToAction("Index", "Administrator")),
                "Преподаватель" => Task.FromResult<IActionResult>(controller.RedirectToAction("Login", "Home"))            };
        }
    }
}
