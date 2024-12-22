using System;
using System.Collections.Generic;

namespace DigitalPortalAcademy.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? PhotoPath { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Curator> Curators { get; set; } = new List<Curator>();

    public virtual ICollection<Curriculum> Curricula { get; set; } = new List<Curriculum>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
