using System;

namespace APITransfer.DTOs
{
    public class UnitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SeatCount { get; set; }
        public Guid AgencyId { get; set; }
        public decimal PricePerSeat { get; set; }
        public string Description { get; set; }
    }
}
