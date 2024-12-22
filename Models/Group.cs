using System;
using System.Collections.Generic;

namespace DigitalPortalAcademy.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public int CuratorId { get; set; }

    public int CourseNumber { get; set; }

    public int SpecialtyId { get; set; }

    public virtual Curator Curator { get; set; } = null!;

    public virtual Specialty Specialty { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
