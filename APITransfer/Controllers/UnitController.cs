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
    public class UnitController : ControllerBase
    {
        private readonly IUnitRepository _unitRepository;

        public UnitController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUnits()
        {
            var units = await _unitRepository.GetAllUnitsAsync();
            var unitDtos = units.Select(u => new UnitDto
            {
                Id = u.Id,
                Name = u.Name,
                SeatCount = u.SeatCount,
                AgencyId = u.AgencyId,
                PricePerSeat = u.PricePerSeat,
                Description = u.Description
            });

            return Ok(new ResponseHelper<IEnumerable<UnitDto>>(true, "Units retrieved successfully", unitDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnitById(Guid id)
        {
            var unit = await _unitRepository.GetUnitByIdAsync(id);
            if (unit == null)
                return NotFound(new ResponseHelper<string>(false, "Unit not found"));

            var unitDto = new UnitDto
            {
                Id = unit.Id,
                Name = unit.Name,
                SeatCount = unit.SeatCount,
                AgencyId = unit.AgencyId,
                PricePerSeat = unit.PricePerSeat,
                Description = unit.Description
            };

            return Ok(new ResponseHelper<UnitDto>(true, "Unit retrieved successfully", unitDto));
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit([FromBody] UnitDto unitDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            var unit = new Unit
            {
                Name = unitDto.Name,
                SeatCount = unitDto.SeatCount,
                AgencyId = unitDto.AgencyId,
                PricePerSeat = unitDto.PricePerSeat,
                Description = unitDto.Description
            };

            await _unitRepository.AddUnitAsync(unit);
            return Ok(new ResponseHelper<UnitDto>(true, "Unit added successfully", unitDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(Guid id, [FromBody] UnitDto unitDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseHelper<string>(false, "Invalid data"));

            var unit = await _unitRepository.GetUnitByIdAsync(id);
            if (unit == null)
                return NotFound(new ResponseHelper<string>(false, "Unit not found"));

            unit.Name = unitDto.Name;
            unit.SeatCount = unitDto.SeatCount;
            unit.AgencyId = unitDto.AgencyId;
            unit.PricePerSeat = unitDto.PricePerSeat;
            unit.Description = unitDto.Description;

            await _unitRepository.UpdateUnitAsync(unit);
            return Ok(new ResponseHelper<UnitDto>(true, "Unit updated successfully", unitDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            var unit = await _unitRepository.GetUnitByIdAsync(id);
            if (unit == null)
                return NotFound(new ResponseHelper<string>(false, "Unit not found"));

            await _unitRepository.DeleteUnitAsync(id);
            return Ok(new ResponseHelper<string>(true, "Unit deleted successfully"));
        }
    }
}
