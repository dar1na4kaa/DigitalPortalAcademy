using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class Announcement
{
    [Key]
    [Column("AnnouncementID")]
    public int AnnouncementId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Column("AuthorID")]
    public int AuthorId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpirationDate { get; set; }

    public bool? IsActive { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Announcements")]
    public virtual User Author { get; set; } = null!;
}
