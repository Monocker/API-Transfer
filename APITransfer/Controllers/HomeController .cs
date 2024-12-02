using APITransfer.DTOs;
using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using APITransfer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeRepository _homeRepository;
        private readonly IAgencyRepository _agencyRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IZoneRepository _zoneRepository;
        private readonly IPickupRepository _pickupRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IReservationRepository _reservationRepository;


        public HomeController(
            IHomeRepository homeRepository, 
            IAgencyRepository agencyRepository, 
            IHotelRepository hotelRepository,
            IUnitRepository unitRepository,
            IZoneRepository zoneRepository,
            IPickupRepository pickupRepository,
            IStoreRepository storeRepository,
            IReservationRepository reservationRepository
            )

        {
            _homeRepository = homeRepository;
            _agencyRepository = agencyRepository;
            _hotelRepository = hotelRepository;
            _unitRepository = unitRepository;
            _zoneRepository = zoneRepository;
            _pickupRepository = pickupRepository;
            _storeRepository = storeRepository;
            _reservationRepository = reservationRepository;
        }

        [HttpGet("get-all-info")]
        public async Task<IActionResult> GetAllInfo()
        {
            try
            {
                var agencies = await _agencyRepository.GetAllAgenciesAsync();
                var hotels = await _hotelRepository.GetAllHotelsAsync();
                var units = await _unitRepository.GetAllUnitsAsync();
                var zones = await _zoneRepository.GetAllZonesAsync();
                var pickups = await _pickupRepository.GetAllPickupsAsync();
                var stores = await _storeRepository.GetAllStoresAsync();

                var response = new
                {
                    Agencies = agencies,
                    Hotels = hotels,
                    Stores = stores,
                    Units = units,
                    Zones = zones,
                    Pickups = pickups
                };

                return Ok(new ResponseHelper<object>(true, "Información recuperada con éxito", response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHelper<string>(false, "Error al recuperar la información", ex.Message));
            }
        }

        [HttpGet("unit-availability")]
        public async Task<IActionResult> GetUnitAvailability(Guid unitId, string pickupTime, DateTime reservationDate, Guid hotelId)
        {
            try
            {
                // Total de asientos en la unidad
                var unit = await _unitRepository.GetUnitByIdAsync(unitId);
                if (unit == null)
                    return NotFound(new ResponseHelper<string>(false, "Unidad no encontrada"));

                // Consultar las reservas para la unidad, hora y fecha específicas
                var reservations = await _reservationRepository.GetReservationsByUnitAndPickup(unitId, pickupTime, reservationDate, hotelId);

                // Calcular estadísticas
                var totalSeats = unit.SeatCount; // Ejemplo: suponiendo que la unidad tiene este campo
                var occupiedSeats = reservations.Count(r => r.Status == "paid");
                var pendingSeats = reservations.Count(r => r.Status == "pending");
                var availableSeats = totalSeats - occupiedSeats;

                var response = new
                {
                    Unit = new { unit.Id, unit.Name },
                    TotalSeats = totalSeats,
                    OccupiedSeats = occupiedSeats,
                    PendingSeats = pendingSeats,
                    AvailableSeats = availableSeats
                };

                return Ok(new ResponseHelper<object>(true, "Disponibilidad de la unidad obtenida con éxito", response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHelper<string>(false, "Error al obtener la disponibilidad", ex.Message));
            }
        }


    }
}
