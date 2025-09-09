using Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("persons");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.IdNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(p => p.Address)
                .HasMaxLength(200);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Indexes
            builder.HasIndex(p => p.IdNumber)
                .IsUnique();
        }
    }
}