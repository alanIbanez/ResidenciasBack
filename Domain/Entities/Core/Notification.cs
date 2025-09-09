using Domain.Entities.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Core
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        public int TargetUserId { get; set; }

        public int? SourceUserId { get; set; }

        public int? ExitId { get; set; }

        public int? EventId { get; set; }

        public bool Read { get; set; } = false;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReadAt { get; set; }

        // Navigation properties
        [ForeignKey("TargetUserId")]
        public virtual User TargetUser { get; set; }

        [ForeignKey("SourceUserId")]
        public virtual User SourceUser { get; set; }

        [ForeignKey("ExitId")]
        public virtual Exit.Exit Exit { get; set; }

        [ForeignKey("EventId")]
        public virtual Event.Event Event { get; set; }
    }
}