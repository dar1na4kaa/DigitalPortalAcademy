using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

[Index("ClassTypeName", Name = "UQ__ClassTyp__184FD14328FBF07C", IsUnique = true)]
public partial class ClassType
{
    [Key]
    [Column("ClassTypeID")]
    public int ClassTypeId { get; set; }

    [StringLength(50)]
    public string ClassTypeName { get; set; } = null!;

    [InverseProperty("ClassType")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
