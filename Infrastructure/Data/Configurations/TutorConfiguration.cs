using Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
    {
        public void Configure(EntityTypeBuilder<Tutor> builder)
        {
            builder.ToTable("tutors");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Specialty)
                .HasMaxLength(100);

            builder.Property(t => t.YearsOfExperience)
                .HasDefaultValue(0);

            builder.Property(t => t.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(t => t.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(t => t.Person)
                .WithOne()
                .HasForeignKey<Tutor>(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Residents)
                .WithOne(r => r.Tutor)
                .HasForeignKey(r => r.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(t => t.PersonId)
                .IsUnique();
        }
    }
}