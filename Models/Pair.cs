using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class Pair
{
    [Key]
    [Column("PairID")]
    public int PairId { get; set; }

    [Column("TeacherSubjectID")]
    public int TeacherSubjectId { get; set; }

    [Column("GroupID")]
    public int GroupId { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("Pairs")]
    public virtual Group Group { get; set; } = null!;

    [InverseProperty("Pair")]
    public virtual ICollection<MarksReport> MarksReports { get; set; } = new List<MarksReport>();

    [InverseProperty("Pair")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    [ForeignKey("TeacherSubjectId")]
    [InverseProperty("Pairs")]
    public virtual TeacherSubject TeacherSubject { get; set; } = null!;
}
