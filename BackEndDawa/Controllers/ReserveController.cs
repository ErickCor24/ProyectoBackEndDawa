using BackEndDawa.Models;
using BackEndDawa.Services.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndDawa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "client")]
    public class ReserveController : ControllerBase
    {
        private readonly IReserve _reserveService;

        public ReserveController(IReserve reserveService)
        {
            _reserveService = reserveService;
        }

        [HttpPost]
        public async Task<IActionResult> PostReserve([FromBody] Reserve reserve)
        {
            if (reserve.PickupDate >= reserve.DropoffDate)
                return BadRequest("La fecha de entrega debe ser posterior a la de retiro.");

            var result = await _reserveService.CreateReserveAsync(reserve);
            if (result == null)
                return Conflict("Conflicto de fechas o vehículo no disponible.");

            return CreatedAtAction(nameof(PostReserve), new { id = result.Id }, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetReserves()
        {
            var reserves = await _reserveService.GetAllReservesAsync();
            return Ok(reserves);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReserveById(int id)
        {
            var reserve = await _reserveService.GetReserveByIdAsync(id);
            if (reserve == null)
                return NotFound();

            return Ok(reserve);
        }

        [HttpPut("{id}")]
        //[Authorize] // opcional si ya protegiste la ruta
        public async Task<IActionResult> PutReserve(int id, Reserve reserve)
        {
            if (id != reserve.Id)
                return BadRequest("El ID de la reserva no coincide.");

            try
            {
                var result = await _reserveService.UpdateReserveAsync(reserve);
                if (!result)
                    return Conflict("Conflicto de fechas o vehículo no disponible.");

                return NoContent(); // 204 actualizado con éxito
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        [Authorize] // Opcional, si ya estás usando JWT
        public async Task<IActionResult> DeleteReserve(int id)
        {
            try
            {
                var deleted = await _reserveService.DeleteReserveAsync(id);
                if (!deleted)
                    return NotFound("Reserva no encontrada.");

                return NoContent(); // 204 OK sin contenido
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la reserva: {ex.Message}");
            }
        }

    }
}
