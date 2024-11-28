using System;
using System.ComponentModel.DataAnnotations;

namespace APITransfer.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; } // FK a Users
        public User User { get; set; }

        [Required]
        public Guid ZoneId { get; set; } // FK a Zonas
        public Zone Zone { get; set; }

        [Required]
        public Guid AgencyId { get; set; } // FK a Agencias
        public Agency Agency { get; set; }

        [Required]
        public Guid HotelId { get; set; } // FK a Hoteles
        public Hotel Hotel { get; set; }

        [Required]
        public Guid UnitId { get; set; } // FK a Units
        public Unit Unit { get; set; }

        [Required]
        public int SeatNumber { get; set; } // Número del asiento reservado

        [Required]
        public DateTime PickupTime { get; set; } // Hora de recogida

        [Required]
        public DateTime ReservationDate { get; set; } // Fecha de reserva

        [Required]
        [MaxLength(100)]
        public string ClientName { get; set; } // Nombre del cliente

        [MaxLength(500)]
        public string Observations { get; set; } // Observaciones adicionales
    }
}
