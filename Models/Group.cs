using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class Group
{
    [Key]
    [Column("GroupID")]
    public int GroupId { get; set; }

    [StringLength(100)]
    public string GroupName { get; set; } = null!;

    [Column("CuratorID")]
    public int CuratorId { get; set; }

    [Column("SpecialtyID")]
    public int SpecialtyId { get; set; }

    [ForeignKey("CuratorId")]
    [InverseProperty("Groups")]
    public virtual Employee Curator { get; set; } = null!;

    [InverseProperty("Group")]
    public virtual ICollection<Pair> Pairs { get; set; } = new List<Pair>();

    [ForeignKey("SpecialtyId")]
    [InverseProperty("Groups")]
    public virtual Specialty Specialty { get; set; } = null!;

    [InverseProperty("Group")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
