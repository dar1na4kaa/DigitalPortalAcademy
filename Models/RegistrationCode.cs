using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DigitalPortalAcademy.Models;

public partial class RegistrationCode
{
    [Key]
    [StringLength(255)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    public bool? IsUsed { get; set; }
}
