using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndDawa.Models;
using BackEndDawa.Services.Ports;

namespace BackEndDawa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehicles()
        {
            try
            {
                var vehicles = await _vehicleService.GetAllVehiclesAsync();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error in server: {ex.Message}");
            }
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                return vehicle == null ? NotFound() : Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error in server: {ex.Message}");
            }
        }

        [HttpGet("by-company/{companyId}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetByCompany(int companyId)
        {
            try
            {
                var vehicles = await _vehicleService.GetVehiclesByCompanyAsync(companyId);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error in server: {ex.Message}");
            }
        }

        [HttpGet("by-field/{field}/{value}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetByField(string field, string value)
        {
            try
            {
                var vehicles = await _vehicleService.GetVehiclesByField(field, value);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error in server: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicle);
                return CreatedAtAction(nameof(GetById), new { id = createdVehicle.Id }, createdVehicle);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error: " + dbEx.InnerException?.Message ?? dbEx.Message, dbEx);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error in server: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _vehicleService.UpdateVehicleAsync(vehicle);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error in server: {ex.Message}");
            }
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> LogicalDelete(int id)
        {
            try
            {
                var result = await _vehicleService.LogicalDeleteVehicleAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error in server: {ex.Message}");
            }
        }
    }
}
