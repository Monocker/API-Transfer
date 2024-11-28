using APITransfer.Helpers;
using APITransfer.Interfaces.Repositories;
using APITransfer.Models;
using Microsoft.AspNetCore.Mvc;

namespace APITransfer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgencyController : ControllerBase
    {
        private readonly IAgencyRepository _agencyRepository;

        public AgencyController(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAgencies()
        {
            var agencies = await _agencyRepository.GetAllAgenciesAsync();
            return Ok(new ResponseHelper<IEnumerable<Agency>>(true, "Agencies retrieved successfully", agencies));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgencyById(Guid id)
        {
            var agency = await _agencyRepository.GetAgencyByIdAsync(id);
            if (agency == null)
                return NotFound(new ResponseHelper<string>(false, "Agency not found"));

            return Ok(new ResponseHelper<Agency>(true, "Agency retrieved successfully", agency));
        }

        [HttpPost]
        public async Task<IActionResult> AddAgency([FromBody] Agency agency)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            await _agencyRepository.AddAgencyAsync(agency);
            return Ok(new ResponseHelper<Agency>(true, "Agency added successfully", agency));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgency(Guid id, [FromBody] Agency agency)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            var existingAgency = await _agencyRepository.GetAgencyByIdAsync(id);
            if (existingAgency == null)
                return NotFound(new ResponseHelper<string>(false, "Agency not found"));

            existingAgency.Name = agency.Name;
            await _agencyRepository.UpdateAgencyAsync(existingAgency);

            return Ok(new ResponseHelper<Agency>(true, "Agency updated successfully", existingAgency));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgency(Guid id)
        {
            var agency = await _agencyRepository.GetAgencyByIdAsync(id);
            if (agency == null)
                return NotFound(new ResponseHelper<string>(false, "Agency not found"));

            await _agencyRepository.DeleteAgencyAsync(id);
            return Ok(new ResponseHelper<string>(true, "Agency deleted successfully"));
        }
    }
}
