using System;
using System.Collections.Generic;

namespace DigitalPortalAcademy.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public int? UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string Phone { get; set; } = null!;

    public int? CycleCommissionId { get; set; }

    public virtual CycleCommission? CycleCommission { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

}
