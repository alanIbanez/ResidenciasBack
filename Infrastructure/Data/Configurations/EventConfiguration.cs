using Domain.Entities.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.Location)
                .HasMaxLength(200);

            builder.Property(e => e.EventDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(e => e.StartTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(e => e.EndTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(e => e.Preceptor)
                .WithMany()
                .HasForeignKey(e => e.PreceptorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Attendances)
                .WithOne(a => a.Event)
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(e => e.PreceptorId);
            builder.HasIndex(e => e.EventDate);
        }
    }
}