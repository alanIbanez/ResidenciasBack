using Domain.Entities.Academic;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Exit
{
    [Table("Exit")]
    public class Exit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ResidentId { get; set; }

        [Required]
        public int ExitTypeId { get; set; }

        [Required]
        public int ExitStatusId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Reason { get; set; }

        [Required]
        public DateTime ExitDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public TimeSpan ExitTime { get; set; }

        [Required]
        public TimeSpan ReturnTime { get; set; }

        [Required]
        [MaxLength(255)]
        public string ExitToken { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        // Navigation properties
        [ForeignKey("ResidentId")]
        public virtual Resident Resident { get; set; }

        [ForeignKey("ExitTypeId")]
        public virtual ExitType ExitType { get; set; }

        [ForeignKey("ExitStatusId")]
        public virtual ExitStatus ExitStatus { get; set; }

        public virtual ICollection<ExitAuthorization> Authorizations { get; set; }

        public Exit()
        {
            Authorizations = new HashSet<ExitAuthorization>();
        }
    }
}