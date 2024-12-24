namespace DigitalPortalAcademy.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }

        public int? UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string Phone { get; set; } = null!;

        public virtual User? User { get; set; }

    }
}
