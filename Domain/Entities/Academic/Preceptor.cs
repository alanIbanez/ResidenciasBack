using Domain.Entities.Core;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Academic
{
    [Table("Preceptor")]
    public class Preceptor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PreceptorTypeId { get; set; }

        [Required]
        public int ShiftId { get; set; }

        public bool Active { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PreceptorTypeId")]
        public virtual PreceptorType PreceptorType { get; set; }

        [ForeignKey("ShiftId")]
        public virtual Shift Shift { get; set; }

        public virtual ICollection<Exit.ExitAuthorization> ExitAuthorizations { get; set; }
        public virtual ICollection<Event.Event> Events { get; set; }
        public virtual ICollection<Event.Attendance> Attendances { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }

        public Preceptor()
        {
            ExitAuthorizations = new HashSet<Exit.ExitAuthorization>();
            Events = new HashSet<Event.Event>();
            Attendances = new HashSet<Event.Attendance>();
            Incidents = new HashSet<Incident>();
        }
    }
}