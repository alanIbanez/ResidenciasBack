using Domain.Entities.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("attendances");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AttendanceType)
                .IsRequired();

            builder.Property(a => a.AttendanceDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(a => a.Comments)
                .HasMaxLength(500);

            builder.Property(a => a.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(a => a.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(a => a.Event)
                .WithMany(e => e.Attendances)
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Resident)
                .WithMany()
                .HasForeignKey(a => a.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Preceptor)
                .WithMany()
                .HasForeignKey(a => a.PreceptorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(a => a.EventId);
            builder.HasIndex(a => a.ResidentId);
            builder.HasIndex(a => a.PreceptorId);
            builder.HasIndex(a => a.AttendanceDate);
        }
    }
}