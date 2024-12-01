namespace APITransfer.DTOs
{
    public class PickupDto
    {
        public Guid Id { get; set; }
        public string PickupTime { get; set; } // Formateado como HH:mm
        public Guid HotelId { get; set; }
    }
}
