using APITransfer.DTOs;
using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        [HttpGet("get-all-info")]
        public async Task<IActionResult> GetAllInfo()
        {
            try
            {
                var agencies = await _homeRepository.GetAllAgenciesAsync();
                var hotels = await _homeRepository.GetAllHotelsAsync();
                var units = await _homeRepository.GetAllUnitsAsync();
                var zones = await _homeRepository.GetAllZonesAsync();

                var response = new
                {
                    Agencies = agencies,
                    Hotels = hotels,
                    Units = units,
                    Zones = zones
                };

                return Ok(new ResponseHelper<object>(true, "Información recuperada con éxito", response));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseHelper<string>(false, "Error al recuperar la información", ex.Message));
            }
        }
    }
}
