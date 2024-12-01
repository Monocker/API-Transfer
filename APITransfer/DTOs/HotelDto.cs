using System.ComponentModel.DataAnnotations;

namespace APITransfer.DTOs
{
    public class HotelDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public Guid ZoneId { get; set; }
    }
}
