using Domain.Entities.Academic;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Exit
{
    [Table("ExitAuthorization")]
    public class ExitAuthorization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ExitId { get; set; }

        public int? TutorId { get; set; }

        public int? PreceptorId { get; set; }

        public int? GuardId { get; set; }

        [Required]
        public bool Authorized { get; set; } = false;

        [MaxLength(255)]
        public string Comment { get; set; }

        public DateTime? AuthorizationDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ExitId")]
        public virtual Exit Exit { get; set; }

        [ForeignKey("TutorId")]
        public virtual Tutor Tutor { get; set; }

        [ForeignKey("PreceptorId")]
        public virtual Preceptor Preceptor { get; set; }

        [ForeignKey("GuardId")]
        public virtual Guard Guard { get; set; }
    }
}