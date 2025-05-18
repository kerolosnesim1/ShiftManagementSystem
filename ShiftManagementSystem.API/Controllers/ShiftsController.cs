using Microsoft.AspNetCore.Mvc;
using ShiftManagementSystem.Application.DTOs.Shifts;
using ShiftManagementSystem.Application.Services.Shifts;
using System;
using System.Threading.Tasks;

namespace ShiftManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftsController : ControllerBase
    {
        private readonly ShiftService _shiftService;

        public ShiftsController(ShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShifts()
        {
            var shifts = await _shiftService.GetAllShiftsAsync();
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShiftById(Guid id)
        {
            var shift = await _shiftService.GetShiftByIdAsync(id);
            if (shift == null)
                return NotFound();
            return Ok(shift);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift([FromBody] CreateShiftDto shiftDto)
        {
            var newShift = await _shiftService.CreateShiftAsync(shiftDto);
            return CreatedAtAction(nameof(GetShiftById), new { id = newShift.Id }, newShift);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(Guid id, [FromBody] UpdateShiftDto shiftDto)
        {
            await _shiftService.UpdateShiftAsync(id, shiftDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(Guid id)
        {
            await _shiftService.DeleteShiftAsync(id);
            return NoContent();
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetShiftsByDate(DateTime date)
        {
            var shifts = await _shiftService.GetShiftsByDateAsync(date);
            return Ok(shifts);
        }
    }
}