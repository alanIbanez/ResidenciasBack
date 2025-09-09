using Domain.Entities;
using Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets para todas las entidades
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Preceptor> Preceptors { get; set; }
        public DbSet<PreceptorType> PreceptorTypes { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Guard> Guards { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<ResidentType> ResidentTypes { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones de Fluent API
            ConfigureUser(modelBuilder);
            ConfigurePerson(modelBuilder);
            ConfigureNotification(modelBuilder);
            ConfigureRole(modelBuilder);
            ConfigurePreceptor(modelBuilder);
            ConfigureTutor(modelBuilder);
            ConfigureGuard(modelBuilder);
            ConfigureResident(modelBuilder);

            // Datos iniciales
            SeedData(modelBuilder);
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(u => u.NotificationToken).HasMaxLength(255);
                entity.Property(u => u.NavigationToken).HasMaxLength(255);
                entity.Property(u => u.Active).HasDefaultValue(true);
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relaciones
                entity.HasOne(u => u.Person)
                      .WithOne(p => p.User)
                      .HasForeignKey<User>(u => u.PersonId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Índices
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.NavigationToken).IsUnique().HasFilter("[NavigationToken] IS NOT NULL");
            });
        }

        private void ConfigurePerson(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.DNI).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(150);
                entity.Property(p => p.Phone).HasMaxLength(20);
                entity.Property(p => p.Address).HasMaxLength(200);
                entity.Property(p => p.Active).HasDefaultValue(true);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Índices
                entity.HasIndex(p => p.DNI).IsUnique();
                entity.HasIndex(p => p.Email).IsUnique();
            });
        }

        private void ConfigureNotification(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(n => n.Id);
                entity.Property(n => n.Title).IsRequired().HasMaxLength(100);
                entity.Property(n => n.Message).IsRequired().HasMaxLength(500);
                entity.Property(n => n.Type).IsRequired().HasMaxLength(50);
                entity.Property(n => n.Read).HasDefaultValue(false);
                entity.Property(n => n.SentAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relaciones
                entity.HasOne(n => n.TargetUser)
                      .WithMany(u => u.ReceivedNotifications)
                      .HasForeignKey(n => n.TargetUserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(n => n.SourceUser)
                      .WithMany(u => u.SentNotifications)
                      .HasForeignKey(n => n.SourceUserId)
                      .OnDelete(DeleteBehavior.SetNull);

                // Índices
                entity.HasIndex(n => n.TargetUserId);
                entity.HasIndex(n => n.SourceUserId);
                entity.HasIndex(n => n.Type);
                entity.HasIndex(n => n.SentAt);
                entity.HasIndex(n => n.Read);
            });
        }

        private void ConfigureRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name).IsRequired().HasMaxLength(50);
                entity.Property(r => r.Description).HasMaxLength(200);
                entity.Property(r => r.Active).HasDefaultValue(true);
                entity.Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Índices
                entity.HasIndex(r => r.Name).IsUnique();
            });
        }

        private void ConfigurePreceptor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Preceptor>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Active).HasDefaultValue(true);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relaciones
                entity.HasOne(p => p.User)
                      .WithOne(u => u.Preceptor)
                      .HasForeignKey<Preceptor>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.PreceptorType)
                      .WithMany(pt => pt.Preceptors)
                      .HasForeignKey(p => p.PreceptorTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Shift)
                      .WithMany(s => s.Preceptors)
                      .HasForeignKey(p => p.ShiftId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureTutor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Active).HasDefaultValue(true);
                entity.Property(t => t.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relaciones
                entity.HasOne(t => t.User)
                      .WithOne(u => u.Tutor)
                      .HasForeignKey<Tutor>(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureGuard(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guard>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Active).HasDefaultValue(true);
                entity.Property(g => g.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relaciones
                entity.HasOne(g => g.User)
                      .WithOne(u => u.Guard)
                      .HasForeignKey<Guard>(g => g.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(g => g.Shift)
                      .WithMany(s => s.Guards)
                      .HasForeignKey(g => g.ShiftId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureResident(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resident>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Active).HasDefaultValue(true);
                entity.Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relaciones
                entity.HasOne(r => r.User)
                      .WithOne(u => u.Resident)
                      .HasForeignKey<Resident>(r => r.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.ResidentType)
                      .WithMany(rt => rt.Residents)
                      .HasForeignKey(r => r.ResidentTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Tutor)
                      .WithMany(t => t.Residents)
                      .HasForeignKey(r => r.TutorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Roles iniciales
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Administrador del sistema", Active = true, CreatedAt = DateTime.UtcNow },
                new Role { Id = 2, Name = "Preceptor", Description = "Preceptor de la residencia", Active = true, CreatedAt = DateTime.UtcNow },
                new Role { Id = 3, Name = "Tutor", Description = "Tutor de residentes", Active = true, CreatedAt = DateTime.UtcNow },
                new Role { Id = 4, Name = "Guard", Description = "Guardia de seguridad", Active = true, CreatedAt = DateTime.UtcNow },
                new Role { Id = 5, Name = "Resident", Description = "Residente", Active = true, CreatedAt = DateTime.UtcNow }
            );

            // Tipos de Preceptor
            modelBuilder.Entity<PreceptorType>().HasData(
                new PreceptorType { Id = 1, Name = "Jefe", Description = "Preceptor jefe", Active = true, CreatedAt = DateTime.UtcNow },
                new PreceptorType { Id = 2, Name = "Asistente", Description = "Preceptor asistente", Active = true, CreatedAt = DateTime.UtcNow }
            );

            // Tipos de Residentes
            modelBuilder.Entity<ResidentType>().HasData(
                new ResidentType { Id = 1, Name = "Regular", Description = "Residente regular", Active = true, CreatedAt = DateTime.UtcNow },
                new ResidentType { Id = 2, Name = "Especial", Description = "Residente especial", Active = true, CreatedAt = DateTime.UtcNow },
                new ResidentType { Id = 3, Name = "Temporal", Description = "Residente temporal", Active = true, CreatedAt = DateTime.UtcNow },
                new ResidentType { Id = 4, Name = "Practicante", Description = "Residente practicante", Active = true, CreatedAt = DateTime.UtcNow }
            );

            // Turnos
            modelBuilder.Entity<Shift>().HasData(
                new Shift { Id = 1, Name = "Matutino", Description = "Turno de la mañana", StartTime = TimeSpan.FromHours(6), EndTime = TimeSpan.FromHours(14), Active = true, CreatedAt = DateTime.UtcNow },
                new Shift { Id = 2, Name = "Vespertino", Description = "Turno de la tarde", StartTime = TimeSpan.FromHours(14), EndTime = TimeSpan.FromHours(22), Active = true, CreatedAt = DateTime.UtcNow },
                new Shift { Id = 3, Name = "Nocturno", Description = "Turno de la noche", StartTime = TimeSpan.FromHours(22), EndTime = TimeSpan.FromHours(6), Active = true, CreatedAt = DateTime.UtcNow }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Manejar fechas de modificación automáticamente
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Person && (e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Person)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}