using Microsoft.EntityFrameworkCore;
using APITransfer.Models;
using System.Collections.Generic;

namespace APITransfer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Zone> Zones { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Unit> Units { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar generación automática de Guid
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Role>()
                .Property(r => r.Id)
                .HasDefaultValueSql("NEWID()");

            // Configuración para la columna PricePerSeat en la tabla Units
            modelBuilder.Entity<Unit>()
                .Property(u => u.PricePerSeat)
                .HasColumnType("decimal(10,2)"); // Precisión 10, Escala 2

            // Configurar generación automática de Guid
            modelBuilder.Entity<Unit>()
                .Property(u => u.Id)
                .HasDefaultValueSql("NEWID()");
        }

    }
}
