using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Academic;

namespace Domain.Entities.Event
{
    [Table("Event")]
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public int PreceptorId { get; set; }

        public bool Active { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("PreceptorId")]
        public virtual Preceptor Preceptor { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Core.Notification> Notifications { get; set; }

        public Event()
        {
            Attendances = new HashSet<Attendance>();
            Notifications = new HashSet<Core.Notification>();
        }
    }
}