using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelRepository.GetAllHotelsAsync();
            return Ok(new ResponseHelper<IEnumerable<Hotel>>(true, "Hotels retrieved successfully", hotels));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(id);
            if (hotel == null)
                return NotFound(new ResponseHelper<string>(false, "Hotel not found"));

            return Ok(new ResponseHelper<Hotel>(true, "Hotel retrieved successfully", hotel));
        }

        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] Hotel hotel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            await _hotelRepository.AddHotelAsync(hotel);
            return Ok(new ResponseHelper<Hotel>(true, "Hotel added successfully", hotel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] Hotel hotel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            var existingHotel = await _hotelRepository.GetHotelByIdAsync(id);
            if (existingHotel == null)
                return NotFound(new ResponseHelper<string>(false, "Hotel not found"));

            existingHotel.Name = hotel.Name;
            existingHotel.ZoneId = hotel.ZoneId;
            await _hotelRepository.UpdateHotelAsync(existingHotel);

            return Ok(new ResponseHelper<Hotel>(true, "Hotel updated successfully", existingHotel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(id);
            if (hotel == null)
                return NotFound(new ResponseHelper<string>(false, "Hotel not found"));

            await _hotelRepository.DeleteHotelAsync(id);
            return Ok(new ResponseHelper<string>(true, "Hotel deleted successfully"));
        }
    }
}
