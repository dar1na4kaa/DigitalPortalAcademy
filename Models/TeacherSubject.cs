using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

[Index("TeacherId", "SubjectId", Name = "UQ_TeacherSubject", IsUnique = true)]
public partial class TeacherSubject
{
    [Key]
    public int PareId { get; set; }

    [Column("TeacherID")]
    public int TeacherId { get; set; }

    [Column("SubjectID")]
    public int SubjectId { get; set; }

    [InverseProperty("TeacherSubject")]
    public virtual ICollection<Pair> Pairs { get; set; } = new List<Pair>();

    [ForeignKey("SubjectId")]
    [InverseProperty("TeacherSubjects")]
    public virtual Subject Subject { get; set; } = null!;

    [ForeignKey("TeacherId")]
    [InverseProperty("TeacherSubjects")]
    public virtual Teacher Teacher { get; set; } = null!;
}
