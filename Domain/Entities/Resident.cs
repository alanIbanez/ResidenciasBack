using Domain.Entities.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Resident")]
    public class Resident
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ResidentTypeId { get; set; }

        [Required]
        public int TutorId { get; set; }

        public bool Active { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ResidentTypeId")]
        public virtual ResidentType ResidentType { get; set; }

        [ForeignKey("TutorId")]
        public virtual Tutor Tutor { get; set; }
    }
}