using Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ResidentTypeConfiguration : IEntityTypeConfiguration<ResidentType>
    {
        public void Configure(EntityTypeBuilder<ResidentType> builder)
        {
            builder.ToTable("resident_types");

            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(rt => rt.Description)
                .HasMaxLength(200);

            builder.Property(rt => rt.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(rt => rt.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasMany(rt => rt.Residents)
                .WithOne(r => r.ResidentType)
                .HasForeignKey(r => r.ResidentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(rt => rt.Name)
                .IsUnique();
        }
    }
}