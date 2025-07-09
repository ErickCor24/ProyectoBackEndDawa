using BackEndDawa.Models;
using BackEndDawa.Services.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndDawa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/client - Obtener todos los clientes
        [HttpGet]
        [Authorize(Roles = "client,company")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/client/{id} - Obtener cliente por ID
        [HttpGet("{id}")]
        [Authorize(Roles = "client,company")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                if (client == null)
                    return NotFound(new { message = "Client not found" });
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/client - Crear nuevo cliente (registro, no requiere auth)
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Client>> CreateClient([FromBody] Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdClient = await _clientService.CreateClientAsync(client);
                return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/client/{id} - Actualizar cliente
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<ActionResult<Client>> UpdateClient(int id, [FromBody] Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedClient = await _clientService.UpdateClientAsync(id, client);
                if (updatedClient == null)
                    return NotFound(new { message = "Client not found" });

                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PATCH: api/client/{id} - Eliminación lógica
        [HttpPatch("{id}")]
        [Authorize(Roles = "client")]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                if (client == null)
                    return NotFound(new { message = "Client not found" });

                client.Status = false; // Eliminación lógica
                var updatedClient = await _clientService.UpdateClientAsync(id, client);
                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/client/search - Búsqueda completa
        [HttpGet("search")]
        [Authorize(Roles = "client,company")]
        public async Task<ActionResult<IEnumerable<Client>>> SearchClients(
            [FromQuery] string? name = null,
            [FromQuery] string? email = null,
            [FromQuery] string? ci = null,
            [FromQuery] string? phone = null,
            [FromQuery] bool? status = null)
        {
            try
            {
                var clients = await _clientService.SearchClientsAsync(name, email, ci, phone, status);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/client/ci/{ci} - Buscar por CI específico
        [HttpGet("ci/{ci}")]
        [Authorize(Roles = "client")]
        public async Task<ActionResult<Client>> GetClientByCi(string ci)
        {
            try
            {
                var client = await _clientService.GetClientByCiAsync(ci);
                if (client == null)
                    return NotFound(new { message = "Client not found" });
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
