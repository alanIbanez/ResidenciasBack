using Domain.Entities.Core;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Academic
{
    [Table("Guard")]
    public class Guard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ShiftId { get; set; }

        public bool Active { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ShiftId")]
        public virtual Shift Shift { get; set; }

        public virtual ICollection<Exit.ExitAuthorization> ExitAuthorizations { get; set; }

        public Guard()
        {
            ExitAuthorizations = new HashSet<Exit.ExitAuthorization>();
        }
    }
}