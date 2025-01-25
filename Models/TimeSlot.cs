using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class TimeSlot
{
    [Key]
    [Column("TimeSlotID")]
    public int TimeSlotId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [InverseProperty("TimeSlot")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
