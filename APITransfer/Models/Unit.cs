using System;
using System.ComponentModel.DataAnnotations;

namespace APITransfer.Models
{
    public class Unit
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // Nombre de la unidad

        [Required]
        public int SeatCount { get; set; } // Número de asientos en la unidad

        [Required]
        public Guid AgencyId { get; set; } // FK a Agencias
        public Agency Agency { get; set; } // Relación con la agencia

        [Required]
        public decimal PricePerSeat { get; set; } // Precio por asiento

        [MaxLength(500)]
        public string Description { get; set; } // Descripción de la unidad (opcional)
    }
}
