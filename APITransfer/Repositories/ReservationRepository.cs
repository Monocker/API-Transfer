using APITransfer.Data;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITransfer.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Zone)
                .Include(r => r.Agency)
                .Include(r => r.Hotel)
                .Include(r => r.Unit)
                .Include(r => r.Store)
                .ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Zone)
                .Include(r => r.Agency)
                .Include(r => r.Hotel)
                .Include(r => r.Unit)
                .Include(r => r.Store)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsSeatAvailable(Guid unitId, int seatNumber, string pickupTime)
        {
            return !await _context.Reservations.AnyAsync(r =>
                r.UnitId == unitId &&
                r.SeatNumber == seatNumber &&
                r.PickupTime == pickupTime); // Comparar directamente el string
        }


        public async Task<IEnumerable<Reservation>> GetReservationsByUnitAndPickup(Guid unitId, string pickupTime, DateTime reservationDate, Guid hotelId)
        {
            return await _context.Reservations
                .Where(r => r.UnitId == unitId
                            && r.PickupTime == pickupTime
                            && r.ReservationDate.Date == reservationDate.Date
                            && r.HotelId == hotelId
                            )
                .ToListAsync();
        }



    }
}
