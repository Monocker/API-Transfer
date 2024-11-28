using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneRepository _zoneRepository;

        public ZoneController(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllZones()
        {
            var zones = await _zoneRepository.GetAllZonesAsync();
            return Ok(new ResponseHelper<IEnumerable<Zone>>(true, "Zones retrieved successfully", zones));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetZoneById(Guid id)
        {
            var zone = await _zoneRepository.GetZoneByIdAsync(id);
            if (zone == null)
                return NotFound(new ResponseHelper<string>(false, "Zone not found"));

            return Ok(new ResponseHelper<Zone>(true, "Zone retrieved successfully", zone));
        }

        [HttpPost]
        public async Task<IActionResult> AddZone([FromBody] Zone zone)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            await _zoneRepository.AddZoneAsync(zone);
            return Ok(new ResponseHelper<Zone>(true, "Zone added successfully", zone));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateZone(Guid id, [FromBody] Zone zone)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            var existingZone = await _zoneRepository.GetZoneByIdAsync(id);
            if (existingZone == null)
                return NotFound(new ResponseHelper<string>(false, "Zone not found"));

            existingZone.Name = zone.Name;
            await _zoneRepository.UpdateZoneAsync(existingZone);

            return Ok(new ResponseHelper<Zone>(true, "Zone updated successfully", existingZone));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZone(Guid id)
        {
            var zone = await _zoneRepository.GetZoneByIdAsync(id);
            if (zone == null)
                return NotFound(new ResponseHelper<string>(false, "Zone not found"));

            await _zoneRepository.DeleteZoneAsync(id);
            return Ok(new ResponseHelper<string>(true, "Zone deleted successfully"));
        }
    }
}
