using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

[Index("WorkEmail", Name = "UQ__Teachers__A9D10534231CCD58", IsUnique = true)]
[Index("PersonnelNumber", Name = "UQ__Teachers__EC6A9E5C1A09CCDB", IsUnique = true)]
public partial class Teacher
{
    [Key]
    public int TeacherId { get; set; }

    [StringLength(15)]
    public string PersonnelNumber { get; set; } = null!;

    [Column("UserID")]
    public int? UserId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string? MiddleName { get; set; }

    [StringLength(100)]
    public string WorkEmail { get; set; } = null!;

    [StringLength(20)]
    public string Phone { get; set; } = null!;

    public int? CycleCommissionId { get; set; }

    [ForeignKey("CycleCommissionId")]
    [InverseProperty("Teachers")]
    public virtual CycleCommission? CycleCommission { get; set; }

    [InverseProperty("Teacher")]
    public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();

    [ForeignKey("UserId")]
    [InverseProperty("Teachers")]
    public virtual User? User { get; set; }
}
