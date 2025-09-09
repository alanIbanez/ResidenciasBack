using Domain.Entities.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Academic
{
    [Table("Tutor")]
    public class Tutor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public bool Active { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Resident> Residents { get; set; }
        public virtual ICollection<Exit.ExitAuthorization> ExitAuthorizations { get; set; }

        public Tutor()
        {
            Residents = new HashSet<Resident>();
            ExitAuthorizations = new HashSet<Exit.ExitAuthorization>();
        }
    }
}