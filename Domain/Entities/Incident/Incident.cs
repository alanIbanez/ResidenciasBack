using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Academic;

namespace Domain.Entities.Incident
{
    [Table("Incident")]
    public class Incident
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ResidentId { get; set; }

        [Required]
        public int PreceptorId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime IncidentDate { get; set; }

        public TimeSpan? IncidentTime { get; set; }

        [MaxLength(20)]
        public string Severity { get; set; }

        [MaxLength(255)]
        public string ActionTaken { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ResidentId")]
        public virtual Resident Resident { get; set; }

        [ForeignKey("PreceptorId")]
        public virtual Preceptor Preceptor { get; set; }
    }
}