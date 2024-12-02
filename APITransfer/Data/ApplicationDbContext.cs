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
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Pickup> Pickups { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar generación automática de Guid para User
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasDefaultValueSql("NEWID()");

            // Configurar generación automática de Guid para Role
            modelBuilder.Entity<Role>()
                .Property(r => r.Id)
                .HasDefaultValueSql("NEWID()");

            // Configuración para la columna PricePerSeat en la tabla Units
            modelBuilder.Entity<Unit>()
                .Property(u => u.PricePerSeat)
                .HasColumnType("decimal(10,2)"); // Precisión 10, Escala 2

            // Configurar generación automática de Guid para Unit
            modelBuilder.Entity<Unit>()
                .Property(u => u.Id)
                .HasDefaultValueSql("NEWID()");

            // Configurar generación automática de Guid para Reservation
            modelBuilder.Entity<Reservation>()
                .Property(r => r.Id)
                .HasDefaultValueSql("NEWID()");

            // Configurar claves foráneas para Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Zone)
                .WithMany()
                .HasForeignKey(r => r.ZoneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Agency)
                .WithMany()
                .HasForeignKey(r => r.AgencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Hotel)
                .WithMany()
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Unit)
                .WithMany()
                .HasForeignKey(r => r.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            //Claves foráneas:
            //
            //Relacionamos Reservation con User, Zone, Agency, Hotel, y Unit.
            //Configuramos el comportamiento de eliminación(Cascade o Restrict) dependiendo de la relación.
            //
            //Índice único para disponibilidad de asientos:
            //
            //Garantizamos que no se pueda reservar el mismo asiento(SeatNumber) en la misma unidad(UnitId) para la misma hora(PickupTime).
            // Configuración para garantizar la unicidad de los asientos en una unidad para una fecha/hora específica
            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.UnitId, r.SeatNumber, r.PickupTime })
                .IsUnique()
                .HasDatabaseName("IX_Reservation_SeatAvailability");

            modelBuilder.Entity<Reservation>()
    .Property(r => r.PickupTime)
    .HasMaxLength(5) // HH:mm
    .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.ReservationDate)
                .IsRequired();

        }
    }
}
