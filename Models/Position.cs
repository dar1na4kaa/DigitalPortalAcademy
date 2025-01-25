using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

[Index("Name", Name = "UQ__Position__737584F6F61E50A3", IsUnique = true)]
public partial class Position
{
    [Key]
    [Column("PositionID")]
    public int PositionId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("DepartmentID")]
    public int DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Positions")]
    public virtual Department Department { get; set; } = null!;

    [InverseProperty("Position")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
