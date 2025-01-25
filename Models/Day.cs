using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

[Index("DayName", Name = "UQ__Days__04F2C90B79A56CBE", IsUnique = true)]
public partial class Day
{
    [Key]
    [Column("DayID")]
    public int DayId { get; set; }

    [StringLength(20)]
    public string DayName { get; set; } = null!;

    [InverseProperty("Day")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
