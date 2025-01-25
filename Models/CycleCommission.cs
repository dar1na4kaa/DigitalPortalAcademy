using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class CycleCommission
{
    [Key]
    [Column("CycleCommissionID")]
    public int CycleCommissionId { get; set; }

    [StringLength(100)]
    public string CommissionName { get; set; } = null!;

    [InverseProperty("CycleCommission")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
