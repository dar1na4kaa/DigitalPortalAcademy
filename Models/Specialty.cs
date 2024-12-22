using System;
using System.Collections.Generic;

namespace DigitalPortalAcademy.Models;

public partial class Specialty
{
    public int SpecialtyId { get; set; }

    public string SpecialtyName { get; set; } = null!;

    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
