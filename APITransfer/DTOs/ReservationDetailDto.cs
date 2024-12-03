namespace APITransfer.DTOs
{
    public class ReservationDetailDto
    {
        public Guid Id { get; set; }
        public string ZoneName { get; set; }
        public string AgencyName { get; set; }
        public string HotelName { get; set; }
        public string UnitName { get; set; }
        public string StoreName { get; set; }
        public int SeatNumber { get; set; }
        public string PickupTime { get; set; }
        public DateTime ReservationDate { get; set; }
        public string ClientName { get; set; }
        public string Observations { get; set; }
        public int Pax { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string Status { get; set; }
        public int Cupon { get; set; }
    }
}
