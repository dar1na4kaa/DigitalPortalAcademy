using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

[Table("Employee")]
[Index("PersonnelNumber", Name = "UQ__Employee__EC6A9E5CB58100A9", IsUnique = true)]
public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    public long PersonnelNumber { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("PositionID")]
    public int PositionId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string? MiddleName { get; set; }

    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [InverseProperty("Curator")]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    [ForeignKey("PositionId")]
    [InverseProperty("Employees")]
    public virtual Position Position { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Employees")]
    public virtual User? User { get; set; }
}
