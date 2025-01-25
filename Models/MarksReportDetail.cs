using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class MarksReportDetail
{
    [Key]
    [Column("ReportDetailID")]
    public int ReportDetailId { get; set; }

    [Column("ReportID")]
    public int ReportId { get; set; }

    [Column("StudentID")]
    public int StudentId { get; set; }

    public int? Mark { get; set; }

    public int? Absences { get; set; }

    [ForeignKey("ReportId")]
    [InverseProperty("MarksReportDetails")]
    public virtual MarksReport Report { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("MarksReportDetails")]
    public virtual Student Student { get; set; } = null!;
}
