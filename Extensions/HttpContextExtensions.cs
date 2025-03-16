namespace DigitalPortalAcademy.Extensions
{
    public static class HttpContextExtensions
    {
        public static int? GetCurrentUserId(this HttpContext context)
        {
            string? userIdStr = context.Session.GetString("UserId");
            return int.TryParse(userIdStr, out int userId) ? userId : null;
        }

        public static string? GetCurrentUserEmail(this HttpContext context)
        {
            return context.Session.GetString("UserEmail");
        }

        public static string? GetCurrentUserRole(this HttpContext context)
        {
            return context.Session.GetString("UserRole");
        }
    }
}
