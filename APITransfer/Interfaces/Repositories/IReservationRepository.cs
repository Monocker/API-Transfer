using APITransfer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APITransfer.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(Guid id);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(Guid id);
        Task<bool> IsSeatAvailable(Guid unitId, int seatNumber, string pickupTime);
        Task<IEnumerable<Reservation>> GetReservationsByUnitAndPickup(Guid unitId, string pickupTime, DateTime reservationDate, Guid hotelId);

    }
}
