namespace DigitalPortalAcademy.ViewModels
{
    using DigitalPortalAcademy.Models;
    using global::DigitalPortalAcademy.Models;

    namespace DigitalPortalAcademy.Models.ViewModels
    {
        public class ReferenceRequestViewModel
        {
            public int Id { get; set; }
            public string ReferenceType { get; set; } = null!;
            public string? Comment { get; set; }
            public DateTime CreatedAt { get; set; }
            public string? FilePath { get; set; } // Из справки
        }

    }

}
