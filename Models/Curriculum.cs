using System;
using System.Collections.Generic;

namespace DigitalPortalAcademy.Models;

public partial class Curriculum
{
    public int CurriculumId { get; set; }

    public int Course { get; set; }

    public int? SpecialtyId { get; set; }

    public string? PlanFile { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UploadedBy { get; set; }

    public virtual Specialty? Specialty { get; set; }

    public virtual User? UploadedByNavigation { get; set; }
}
