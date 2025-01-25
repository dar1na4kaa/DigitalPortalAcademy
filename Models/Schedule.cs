using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class Schedule
{
    [Key]
    [Column("ScheduleID")]
    public int ScheduleId { get; set; }

    [Column("PairID")]
    public int PairId { get; set; }

    [Column("RoomID")]
    public int RoomId { get; set; }

    [Column("DayID")]
    public int DayId { get; set; }

    [Column("TimeSlotID")]
    public int TimeSlotId { get; set; }

    [Column("ClassTypeID")]
    public int ClassTypeId { get; set; }

    [ForeignKey("ClassTypeId")]
    [InverseProperty("Schedules")]
    public virtual ClassType ClassType { get; set; } = null!;

    [ForeignKey("DayId")]
    [InverseProperty("Schedules")]
    public virtual Day Day { get; set; } = null!;

    [ForeignKey("PairId")]
    [InverseProperty("Schedules")]
    public virtual Pair Pair { get; set; } = null!;

    [ForeignKey("RoomId")]
    [InverseProperty("Schedules")]
    public virtual Room Room { get; set; } = null!;

    [ForeignKey("TimeSlotId")]
    [InverseProperty("Schedules")]
    public virtual TimeSlot TimeSlot { get; set; } = null!;
}
