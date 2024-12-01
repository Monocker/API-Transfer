using APITransfer.DTOs;
using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            var stores = await _storeRepository.GetAllStoresAsync();
            return Ok(new ResponseHelper<IEnumerable<StoreDto>>(true, "Tiendas recuperadas con éxito", stores));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(Guid id)
        {
            var store = await _storeRepository.GetStoreByIdAsync(id);
            if (store == null)
                return NotFound(new ResponseHelper<string>(false, "Tienda no encontrada"));

            return Ok(new ResponseHelper<StoreDto>(true, "Tienda recuperada con éxito", store));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] StoreDto storeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Datos inválidos"));

            var store = new Store
            {
                Id = Guid.NewGuid(),
                Name = storeDto.Name,
                ZoneId = storeDto.ZoneId
            };

            await _storeRepository.AddStoreAsync(store);
            return Ok(new ResponseHelper<Store>(true, "Tienda creada con éxito", store));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(Guid id, [FromBody] StoreDto storeDto)
        {
            var existingStore = await _storeRepository.GetStoreByIdAsync(id);
            if (existingStore == null)
                return NotFound(new ResponseHelper<string>(false, "Tienda no encontrada"));

            var store = new Store
            {
                Id = id,
                Name = storeDto.Name,
                ZoneId = storeDto.ZoneId
            };

            await _storeRepository.UpdateStoreAsync(store);
            return Ok(new ResponseHelper<Store>(true, "Tienda actualizada con éxito", store));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            var store = await _storeRepository.GetStoreByIdAsync(id);
            if (store == null)
                return NotFound(new ResponseHelper<string>(false, "Tienda no encontrada"));

            await _storeRepository.DeleteStoreAsync(id);
            return Ok(new ResponseHelper<string>(true, "Tienda eliminada con éxito"));
        }
    }
}
