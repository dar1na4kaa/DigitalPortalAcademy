using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class Curriculum
{
    [Key]
    [Column("CurriculumID")]
    public int CurriculumId { get; set; }

    public int Course { get; set; }

    [Column("SpecialtyID")]
    public int? SpecialtyId { get; set; }

    [StringLength(255)]
    public string? PlanFilePath { get; set; }

    [ForeignKey("SpecialtyId")]
    [InverseProperty("Curricula")]
    public virtual Specialty? Specialty { get; set; }
}
