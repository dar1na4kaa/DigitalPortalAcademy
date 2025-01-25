using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

[Index("SpecialtyCode", Name = "UQ__Specialt__739F5275FB9108B9", IsUnique = true)]
public partial class Specialty
{
    [Key]
    [Column("SpecialtyID")]
    public int SpecialtyId { get; set; }

    [StringLength(255)]
    public string SpecialtyName { get; set; } = null!;

    [StringLength(10)]
    public string? SpecialtyCode { get; set; }

    [InverseProperty("Specialty")]
    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();

    [InverseProperty("Specialty")]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
