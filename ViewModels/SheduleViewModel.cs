using System.ComponentModel.DataAnnotations;

namespace DigitalPortalAcademy.ViewModels
{
    public class SheduleViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "День")]
        public DayOfWeek Day { get; set; }
        public List<PairViewModel> Pairs { get; set; } = new();

    }
}
