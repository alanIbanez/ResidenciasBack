using Domain.Entities.Incident;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.ToTable("incidents");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(i => i.IncidentType)
                .IsRequired();

            builder.Property(i => i.SeverityLevel)
                .IsRequired();

            builder.Property(i => i.IncidentDate)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(i => i.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(i => i.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(i => i.Resident)
                .WithMany()
                .HasForeignKey(i => i.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Preceptor)
                .WithMany()
                .HasForeignKey(i => i.PreceptorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(i => i.ResidentId);
            builder.HasIndex(i => i.PreceptorId);
            builder.HasIndex(i => i.IncidentDate);
        }
    }
}