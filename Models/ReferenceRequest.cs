using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalPortalAcademy.Models
{
    public class ReferenceRequest
    {
        [Key]
        public int ReferenceRequestId { get; set; }

        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string ReferenceType { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(1000)]
        public string? Comment { get; set; }

        // Навигационное свойство 1:1
        public virtual Reference Reference { get; set; } = null!;

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; } = null!;
    }
}
