using DigitalPortalAcademy.Models;

namespace DigitalPortalAcademy.ViewModels
{
    public class ReportFilterViewModel
    {
        public int SelectedGroupId { get; set; }
        public int SelectedSubjectId { get; set; }

        public List<Group> Groups { get; set; } = new();
        public List<Subject> Subjects { get; set; } = new();
        public List<Student> Students { get; set; } = new();

        public List<ReportDetailViewModel> Details { get; set; } = new();
    }

}
