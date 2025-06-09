using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

[Index("PersonalEmail", Name = "UQ__Students__A9D10534231CCD59", IsUnique = true)]
[Index("StudentNumber", Name = "UQ__Students__A9D10534231CCD58", IsUnique = true)]

public partial class Student
{
    [Key]
    [Column("StudentID")]
    public int StudentId { get; set; }

    [Column("GroupID")]
    public int GroupId { get; set; }

    [StringLength(15)]
    public string StudentNumber { get; set; } = null!;

    [Column("UserID")]
    public int? UserId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string? MiddleName { get; set; }

    [StringLength(20)]
    public string PassportNumber { get; set; } = null!;

    [StringLength(20)]
    public string AttestatNumber { get; set; } = null!;
    [StringLength(100)]
    public string PersonalEmail { get; set; } = null!;

    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [StringLength(255)]
    public string Address { get; set; } = null!;

    [ForeignKey("GroupId")]
    [InverseProperty("Students")]
    public virtual Group Group { get; set; } = null!;

    [InverseProperty("Student")]
    public virtual ICollection<MarksReportDetail> MarksReportDetails { get; set; } = new List<MarksReportDetail>();

    [ForeignKey("UserId")]
    [InverseProperty("Students")]
    public virtual User? User { get; set; }
    [InverseProperty("Student")]
    public virtual ICollection<ReferenceRequest> ReferenceRequests { get; set; } = new HashSet<ReferenceRequest>();
}

