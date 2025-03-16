using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RoleAuthorizeAttribute : Attribute, IActionFilter
{
    private readonly string[] _roles;

    public RoleAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var session = context.HttpContext.Session;
        var userId = session.GetString("UserId");
        var userRole = session.GetString("UserRole");

        if (string.IsNullOrEmpty(userId) || !_roles.Contains(userRole))
        {
            context.Result = new RedirectToActionResult("Login", "Authentication", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
