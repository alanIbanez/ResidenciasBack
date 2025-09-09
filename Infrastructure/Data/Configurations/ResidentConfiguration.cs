using Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
    {
        public void Configure(EntityTypeBuilder<Resident> builder)
        {
            builder.ToTable("residents");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.University)
                .HasMaxLength(100);

            builder.Property(r => r.Career)
                .HasMaxLength(100);

            builder.Property(r => r.Semester)
                .HasMaxLength(20);

            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(r => r.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(r => r.Person)
                .WithOne()
                .HasForeignKey<Resident>(r => r.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.ResidentType)
                .WithMany()
                .HasForeignKey(r => r.ResidentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Tutor)
                .WithMany(t => t.Residents)
                .HasForeignKey(r => r.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Exits)
                .WithOne(e => e.Resident)
                .HasForeignKey(e => e.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(r => r.PersonId)
                .IsUnique();

            builder.HasIndex(r => r.ResidentTypeId);
            builder.HasIndex(r => r.TutorId);
        }
    }
}