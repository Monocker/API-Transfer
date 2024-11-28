using System.ComponentModel.DataAnnotations;

namespace APITransfer.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } // Debe ser Guid

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Guid RoleId { get; set; } // Debe ser Guid
    }
}
