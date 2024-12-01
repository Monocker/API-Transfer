using System.ComponentModel.DataAnnotations;

namespace APITransfer.Models
{
    public class Pickup
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public TimeSpan PickupTime { get; set; } // Representa horas y minutos

        [Required]
        public Guid HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}
