using DigitalPortalAcademy.Models;
using System.ComponentModel.DataAnnotations;

namespace DigitalPortalAcademy.ViewModels
{
    public class EditUserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Логин")]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage = "Обязательное поле")]
        public string RoleName { get; set; } = string.Empty;
        [Display(Name = "Новый пароль")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Отчество")]

        public string? MiddleName { get; set; }
    }



}
