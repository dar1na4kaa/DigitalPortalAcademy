using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DigitalPortalAcademy.ViewModels
{
    public class EditAccountInformationViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
        [Display(Name = "Логин (Email)")]
        public string Login { get; set; }

        [Display(Name = "Роль")]
        public string RoleName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Минимум 6 символов")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердите пароль")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Аватар")]
        public IFormFile? AvatarFile { get; set; }

        public string? CurrentAvatarPath { get; set; }
    }
}