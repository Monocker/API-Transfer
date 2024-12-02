using APITransfer.DTOs;
using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationRepository.GetAllReservationsAsync();
            var reservationDtos = reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                UserId = r.UserId,
                ZoneId = r.ZoneId,
                AgencyId = r.AgencyId,
                HotelId = r.HotelId,
                UnitId = r.UnitId,
                SeatNumber = r.SeatNumber,
                PickupTime = r.PickupTime,
                ReservationDate = r.ReservationDate,
                ClientName = r.ClientName,
                Observations = r.Observations,
                StoreId = r.StoreId,
                Pax = r.Pax,
                Adults = r.Adults,
                Children = r.Children,
                Status = r.Status
            });

            return Ok(new ResponseHelper<IEnumerable<ReservationDto>>(true, "Reservations retrieved successfully", reservationDtos));
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] ReservationDto reservationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            var isSeatAvailable = await _reservationRepository.IsSeatAvailable(reservationDto.UnitId, reservationDto.SeatNumber, reservationDto.PickupTime);
            if (!isSeatAvailable)
                return BadRequest(new ResponseHelper<string>(false, "Seat is already reserved for the specified time"));

            var reservation = new Reservation
            {
                UserId = reservationDto.UserId,
                ZoneId = reservationDto.ZoneId,
                AgencyId = reservationDto.AgencyId,
                HotelId = reservationDto.HotelId,
                UnitId = reservationDto.UnitId,
                SeatNumber = reservationDto.SeatNumber,
                PickupTime = reservationDto.PickupTime,
                ReservationDate = reservationDto.ReservationDate,
                ClientName = reservationDto.ClientName,
                Observations = reservationDto.Observations,
                StoreId = reservationDto.StoreId,
                Pax = reservationDto.Pax,
                Adults = reservationDto.Adults,
                Children = reservationDto.Children,
                Status = reservationDto.Status
            };

            await _reservationRepository.AddReservationAsync(reservation);
            return Ok(new ResponseHelper<string>(true, "Reservation added successfully"));
        }
    }
}
