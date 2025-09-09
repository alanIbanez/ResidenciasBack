using Domain.Entities.Exit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ExitTypeConfiguration : IEntityTypeConfiguration<ExitType>
    {
        public void Configure(EntityTypeBuilder<ExitType> builder)
        {
            builder.ToTable("exit_types");

            builder.HasKey(et => et.Id);

            builder.Property(et => et.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(et => et.Description)
                .HasMaxLength(200);

            builder.Property(et => et.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(et => et.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasMany(et => et.Exits)
                .WithOne(e => e.ExitType)
                .HasForeignKey(e => e.ExitTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(et => et.Name)
                .IsUnique();
        }
    }
}