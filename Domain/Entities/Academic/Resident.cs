using Domain.Entities.Core;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Academic
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

        public virtual ICollection<Exit.Exit> Exits { get; set; }
        public virtual ICollection<Event.Attendance> Attendances { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }

        public Resident()
        {
            Exits = new HashSet<Exit.Exit>();
            Attendances = new HashSet<Event.Attendance>();
            Incidents = new HashSet<Incident>();
        }
    }
}