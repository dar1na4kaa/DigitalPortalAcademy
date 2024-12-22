using System;
using System.Collections.Generic;

namespace DigitalPortalAcademy.Models;

public partial class UniqueCode
{
    public int UniqueCodeId { get; set; }

    public string UniqueCode1 { get; set; } = null!;

    public bool? IsUsed { get; set; }
}
