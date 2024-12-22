using System;
using System.Collections.Generic;

namespace DigitalPortalAcademy.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string HeadOfDepartment { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Curator> Curators { get; set; } = new List<Curator>();
}
