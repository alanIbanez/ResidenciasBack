using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Domain.Entities.Core
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(255)]
        public string NotificationToken { get; set; }

        [MaxLength(255)]
        public string NavigationToken { get; set; }

        public bool Active { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }

        // Navigation properties
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Notification> SentNotifications { get; set; }
        public virtual ICollection<Notification> ReceivedNotifications { get; set; }

        // Related entities
        public virtual Preceptor Preceptor { get; set; }
        public virtual Tutor Tutor { get; set; }
        public virtual Guard Guard { get; set; }
        public virtual Resident Resident { get; set; }

        public User()
        {
            SentNotifications = new HashSet<Notification>();
            ReceivedNotifications = new HashSet<Notification>();
        }
    }
}