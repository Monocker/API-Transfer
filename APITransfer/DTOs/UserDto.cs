namespace APITransfer.DTOs
{
    public class UserDto
    {
        public Guid Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; } 
    }
}
