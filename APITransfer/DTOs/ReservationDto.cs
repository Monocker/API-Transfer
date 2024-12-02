using System;

namespace APITransfer.DTOs
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ZoneId { get; set; }
        public Guid AgencyId { get; set; }
        public Guid HotelId { get; set; }
        public Guid UnitId { get; set; }
        public int SeatNumber { get; set; }
        public DateTime PickupTime { get; set; }
        public DateTime ReservationDate { get; set; }
        public string ClientName { get; set; }
        public string Observations { get; set; }
        public Guid StoreId { get; set; }
        public int Pax { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string Status { get; set; }
    }
}
