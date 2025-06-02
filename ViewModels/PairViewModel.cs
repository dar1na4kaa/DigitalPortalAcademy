using System.ComponentModel.DataAnnotations;

namespace DigitalPortalAcademy.ViewModels
{
    public class PairViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Номер пары")]
        public string NumberPair { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Пара")]
        public string PairName { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Время")]
        public string TimeSlot { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Кабинет")]
        public string Room { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Тип пары")]

        public string ClassType { get; set; }
    }
}
