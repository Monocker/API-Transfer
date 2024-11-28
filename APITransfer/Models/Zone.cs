using System.ComponentModel.DataAnnotations;

namespace APITransfer.Models
{
    public class Zone
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
