using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Academic;

namespace Domain.Entities.Event
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ResidentId { get; set; }

        [Required]
        public int AttendanceTypeId { get; set; }

        public int? EventId { get; set; }

        [Required]
        public int PreceptorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Present { get; set; } = false;

        [MaxLength(255)]
        public string Justification { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ResidentId")]
        public virtual Resident Resident { get; set; }

        [ForeignKey("AttendanceTypeId")]
        public virtual AttendanceType AttendanceType { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        [ForeignKey("PreceptorId")]
        public virtual Preceptor Preceptor { get; set; }
    }
}