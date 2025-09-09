using Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(n => n.NotificationType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(n => n.FcmToken)
                .HasMaxLength(200);

            builder.Property(n => n.NavigationToken)
                .HasMaxLength(200);

            builder.Property(n => n.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(n => n.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(n => n.UserId);
            builder.HasIndex(n => n.NotificationType);
            builder.HasIndex(n => n.IsRead);
        }
    }
}