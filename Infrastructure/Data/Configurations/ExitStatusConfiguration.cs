using BackendNotificaciones.Domain.Entities.Exit;
using Domain.Entities.Exit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ExitStatusConfiguration : IEntityTypeConfiguration<ExitStatus>
    {
        public void Configure(EntityTypeBuilder<ExitStatus> builder)
        {
            builder.ToTable("exit_statuses");

            builder.HasKey(es => es.Id);

            builder.Property(es => es.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(es => es.Description)
                .HasMaxLength(200);

            builder.Property(es => es.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(es => es.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasMany(es => es.Exits)
                .WithOne(e => e.ExitStatus)
                .HasForeignKey(e => e.ExitStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(es => es.Name)
                .IsUnique();
        }
    }
}