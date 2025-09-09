using Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class GuardConfiguration : IEntityTypeConfiguration<Guard>
    {
        public void Configure(EntityTypeBuilder<Guard> builder)
        {
            builder.ToTable("guards");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.EmployeeId)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(g => g.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(g => g.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(g => g.Person)
                .WithOne()
                .HasForeignKey<Guard>(g => g.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.Shift)
                .WithMany()
                .HasForeignKey(g => g.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(g => g.PersonId)
                .IsUnique();

            builder.HasIndex(g => g.EmployeeId)
                .IsUnique();

            builder.HasIndex(g => g.ShiftId);
        }
    }
}