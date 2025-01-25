using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

[Index("DepartmentName", Name = "UQ__Departme__D949CC34DC092056", IsUnique = true)]
public partial class Department
{
    [Key]
    [Column("DepartmentID")]
    public int DepartmentId { get; set; }

    [StringLength(100)]
    public string DepartmentName { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(100)]
    public string Description { get; set; } = null!;

    [InverseProperty("Department")]
    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
}
