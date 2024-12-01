using APITransfer.DTOs;
using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PickupController : ControllerBase
    {
        private readonly IPickupRepository _pickupRepository;

        public PickupController(IPickupRepository pickupRepository)
        {
            _pickupRepository = pickupRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPickups()
        {
            var pickups = await _pickupRepository.GetAllPickupsAsync();
            return Ok(new ResponseHelper<IEnumerable<PickupDto>>(true, "Horas de recogida recuperadas con éxito", pickups));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPickupById(Guid id)
        {
            var pickup = await _pickupRepository.GetPickupByIdAsync(id);
            if (pickup == null)
                return NotFound(new ResponseHelper<string>(false, "Hora de recogida no encontrada"));

            return Ok(new ResponseHelper<PickupDto>(true, "Hora de recogida recuperada con éxito", pickup));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePickup([FromBody] PickupDto pickupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Datos inválidos"));

            var pickup = new Pickup
            {
                Id = Guid.NewGuid(),
                PickupTime = TimeSpan.Parse(pickupDto.PickupTime), // Convertir desde formato HH:mm
                HotelId = pickupDto.HotelId
            };

            await _pickupRepository.AddPickupAsync(pickup);
            return Ok(new ResponseHelper<Pickup>(true, "Hora de recogida creada con éxito", pickup));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePickup(Guid id, [FromBody] PickupDto pickupDto)
        {
            var existingPickup = await _pickupRepository.GetPickupByIdAsync(id);
            if (existingPickup == null)
                return NotFound(new ResponseHelper<string>(false, "Hora de recogida no encontrada"));

            var pickup = new Pickup
            {
                Id = id,
                PickupTime = TimeSpan.Parse(pickupDto.PickupTime), // Convertir desde formato HH:mm
                HotelId = pickupDto.HotelId
            };

            await _pickupRepository.UpdatePickupAsync(pickup);
            return Ok(new ResponseHelper<Pickup>(true, "Hora de recogida actualizada con éxito", pickup));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePickup(Guid id)
        {
            var pickup = await _pickupRepository.GetPickupByIdAsync(id);
            if (pickup == null)
                return NotFound(new ResponseHelper<string>(false, "Hora de recogida no encontrada"));

            await _pickupRepository.DeletePickupAsync(id);
            return Ok(new ResponseHelper<string>(true, "Hora de recogida eliminada con éxito"));
        }
    }
}
