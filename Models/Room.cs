using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class Room
{
    [Key]
    [Column("RoomID")]
    public int RoomId { get; set; }

    [StringLength(100)]
    public string RoomName { get; set; } = null!;

    [Column("BuildingID")]
    public int BuildingId { get; set; }

    public int Capacity { get; set; }

    [ForeignKey("BuildingId")]
    [InverseProperty("Rooms")]
    public virtual Building Building { get; set; } = null!;

    [InverseProperty("Room")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
