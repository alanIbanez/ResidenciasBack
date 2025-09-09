using Domain.Entities.Academic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PreceptorTypeConfiguration : IEntityTypeConfiguration<PreceptorType>
    {
        public void Configure(EntityTypeBuilder<PreceptorType> builder)
        {
            builder.ToTable("preceptor_types");

            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pt => pt.Description)
                .HasMaxLength(200);

            builder.Property(pt => pt.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(pt => pt.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasMany(pt => pt.Preceptors)
                .WithOne(p => p.PreceptorType)
                .HasForeignKey(p => p.PreceptorTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(pt => pt.Name)
                .IsUnique();
        }
    }
}