using Domain.Entities.Exit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ExitConfiguration : IEntityTypeConfiguration<Exit>
    {
        public void Configure(EntityTypeBuilder<Exit> builder)
        {
            builder.ToTable("exits");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.ExitToken)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ExitDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(e => e.ReturnDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(e => e.ExitTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(e => e.ReturnTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(e => e.Reason)
                .HasMaxLength(500);

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(e => e.Resident)
                .WithMany(r => r.Exits)
                .HasForeignKey(e => e.ResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.ExitType)
                .WithMany()
                .HasForeignKey(e => e.ExitTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ExitStatus)
                .WithMany()
                .HasForeignKey(e => e.ExitStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.ExitAuthorizations)
                .WithOne(ea => ea.Exit)
                .HasForeignKey(ea => ea.ExitId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(e => e.ExitToken)
                .IsUnique();

            builder.HasIndex(e => e.ResidentId);
            builder.HasIndex(e => e.ExitTypeId);
            builder.HasIndex(e => e.ExitStatusId);
        }
    }
}