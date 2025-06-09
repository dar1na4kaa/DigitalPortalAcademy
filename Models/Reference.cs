using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalPortalAcademy.Models
{
    public class Reference
    {
        [Key, ForeignKey("ReferenceRequest")]
        public int ReferenceRequestId { get; set; } 

        [Required]
        [StringLength(255)]
        public string FilePath { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ReferenceRequest ReferenceRequest { get; set; } = null!;
    }
}
