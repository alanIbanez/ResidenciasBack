using Domain.Entities.Exit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AuthorizationConfiguration : IEntityTypeConfiguration<ExitAuthorization>
    {
        public void Configure(EntityTypeBuilder<ExitAuthorization> builder)
        {
            builder.ToTable("exit_authorizations");

            builder.HasKey(ea => ea.Id);

            builder.Property(ea => ea.AuthorizationDate)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(ea => ea.Comments)
                .HasMaxLength(500);

            builder.Property(ea => ea.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            builder.Property(ea => ea.UpdatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone");

            // Relationships
            builder.HasOne(ea => ea.Exit)
                .WithMany(e => e.ExitAuthorizations)
                .HasForeignKey(ea => ea.ExitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ea => ea.Authorizer)
                .WithMany()
                .HasForeignKey(ea => ea.AuthorizerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(ea => ea.ExitId);
            builder.HasIndex(ea => ea.AuthorizerId);
            builder.HasIndex(ea => ea.AuthorizationDate);
        }
    }
}