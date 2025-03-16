using DigitalPortalAcademy.Models;
using System.ComponentModel.DataAnnotations;

namespace DigitalPortalAcademy.ViewModels
{
    public class EditUserViewModel
    {
        public int UserId { get; set; }
        public string Login { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string? NewPassword { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
    }



}
