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
        }

    }
}
