using Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable("shifts");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.StartTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(s => s.EndTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(s => s.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(s => s.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasMany(s => s.Preceptors)
                .WithOne(p => p.Shift)
                .HasForeignKey(p => p.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Guards)
                .WithOne(g => g.Shift)
                .HasForeignKey(g => g.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(s => s.Name)
                .IsUnique();
        }
    }
}