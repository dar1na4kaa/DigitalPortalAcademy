using System;
using System.Collections.Generic;

namespace DigitalPortalAcademy.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int? UserId { get; set; }

    public int GroupId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string PassportNumber { get; set; } = null!;

    public string PassportIssuer { get; set; } = null!;

    public string AttestatNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? ParentName { get; set; }

    public string? ParentPhone { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();

    public virtual User? User { get; set; }
}
