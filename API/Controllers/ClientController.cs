using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AleksMediaEmailSystem.API.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _clientService.AddClientAsync(client);
            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingClient = await _clientService.GetClientByIdAsync(id);
            if (existingClient == null) return NotFound();

            existingClient.Name = client.Name;
            existingClient.Email = client.Email;
            existingClient.IsActive = client.IsActive;

            await _clientService.UpdateClientAsync(existingClient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var existingClient = await _clientService.GetClientByIdAsync(id);
            if (existingClient == null) return NotFound();

            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }
    }
}
