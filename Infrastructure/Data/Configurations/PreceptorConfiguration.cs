using Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PreceptorConfiguration : IEntityTypeConfiguration<Preceptor>
    {
        public void Configure(EntityTypeBuilder<Preceptor> builder)
        {
            builder.ToTable("preceptors");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.EmployeeId)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(p => p.Person)
                .WithOne()
                .HasForeignKey<Preceptor>(p => p.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.PreceptorType)
                .WithMany()
                .HasForeignKey(p => p.PreceptorTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Shift)
                .WithMany()
                .HasForeignKey(p => p.ShiftId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Events)
                .WithOne(e => e.Preceptor)
                .HasForeignKey(e => e.PreceptorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Attendances)
                .WithOne(a => a.Preceptor)
                .HasForeignKey(a => a.PreceptorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Incidents)
                .WithOne(i => i.Preceptor)
                .HasForeignKey(i => i.PreceptorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(p => p.PersonId)
                .IsUnique();

            builder.HasIndex(p => p.EmployeeId)
                .IsUnique();

            builder.HasIndex(p => p.PreceptorTypeId);
            builder.HasIndex(p => p.ShiftId);
        }
    }
}