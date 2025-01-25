using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class Building
{
    [Key]
    [Column("BuildingID")]
    public int BuildingId { get; set; }

    [StringLength(100)]
    public string BuildingName { get; set; } = null!;

    [StringLength(255)]
    public string Address { get; set; } = null!;

    [InverseProperty("Building")]
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
