using System;
using System.Collections.Generic;

namespace DigitalPortalAcademy.Models;

public partial class CycleCommission
{
    public int CycleCommissionId { get; set; }

    public string CommissionName { get; set; } = null!;

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
