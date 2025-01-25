using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class Student
{
    [Key]
    [Column("StudentID")]
    public int StudentId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("GroupID")]
    public int GroupId { get; set; }

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string? MiddleName { get; set; }

    [StringLength(20)]
    public string PassportNumber { get; set; } = null!;

    [StringLength(100)]
    public string PassportIssuer { get; set; } = null!;

    [StringLength(20)]
    public string AttestatNumber { get; set; } = null!;

    [StringLength(255)]
    public string Address { get; set; } = null!;

    [StringLength(255)]
    public string? ParentName { get; set; }

    [StringLength(20)]
    public string? ParentPhone { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("Students")]
    public virtual Group Group { get; set; } = null!;

    [InverseProperty("Student")]
    public virtual ICollection<MarksReportDetail> MarksReportDetails { get; set; } = new List<MarksReportDetail>();

    [ForeignKey("UserId")]
    [InverseProperty("Students")]
    public virtual User? User { get; set; }
}
