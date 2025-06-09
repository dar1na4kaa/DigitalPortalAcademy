using System.ComponentModel.DataAnnotations;

namespace DigitalPortalAcademy.ViewModels
{
    public class AnnouncementAddViewModel
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
