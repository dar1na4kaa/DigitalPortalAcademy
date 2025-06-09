using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DigitalPortalAcademy.ViewModels
{
    public class CurriculumAddViewModel
    {
        [Required(ErrorMessage = "Укажите курс")]
        [Range(1, 4, ErrorMessage = "Курс должен быть от 1 до 4")]
        [Display(Name = "Курс")]
        public int Course { get; set; }

        [Required(ErrorMessage = "Выберите специальность")]
        [Display(Name = "Специальность")]
        public int Specialty { get; set; }

        [Required(ErrorMessage = "Загрузите файл учебного плана")]
        [Display(Name = "Учебный план (PDF)")]
        public IFormFile? PlanFile { get; set; }
    }
}
