using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class Subject
{
    [Key]
    [Column("SubjectID")]
    public int SubjectId { get; set; }

    [StringLength(100)]
    public string SubjectName { get; set; } = null!;

    [InverseProperty("Subject")]
    public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();
}
