using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class MarksReport
{
    [Key]
    [Column("ReportID")]
    public int ReportId { get; set; }

    [Column("PairID")]
    public int PairId { get; set; }

    public DateOnly ReportMonth { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Report")]
    public virtual ICollection<MarksReportDetail> MarksReportDetails { get; set; } = new List<MarksReportDetail>();

    [ForeignKey("PairId")]
    [InverseProperty("MarksReports")]
    public virtual Pair Pair { get; set; } = null!;
}
