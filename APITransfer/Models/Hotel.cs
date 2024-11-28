using System.ComponentModel.DataAnnotations;

namespace APITransfer.Models
{
    public class Hotel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public Guid ZoneId { get; set; } 
        public Zone Zone { get; set; }
    }
}
