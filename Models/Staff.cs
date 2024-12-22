namespace DigitalPortalAcademy.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; } // Отчество, если требуется
        public string PhoneNumber { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }

}
