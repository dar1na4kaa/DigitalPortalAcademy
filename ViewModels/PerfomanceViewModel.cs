using Microsoft.AspNetCore.Mvc.Rendering;

namespace DigitalPortalAcademy.ViewModels
{
    public class PerformanceViewModel
    {
        public string SelectedMonth { get; set; } = "все";
        public string SelectedSubject { get; set; }

        public List<SelectListItem> MonthsSelectList { get; set; } = new();

        public List<SelectListItem> SubjectsSelectList { get; set; } = new();

        public List<PerfomanceItemViewModel> PerformanceItems { get; set; } = new();
        public List<string> AvailableSubjects { get; set; } = new();
        public bool HasDebt { get; internal set; }
    }
}
