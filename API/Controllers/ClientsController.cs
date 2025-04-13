using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Core.DTOs.Filters;
using Infrastructure.Interfaces;

namespace API.Controllers
{
    /// <summary>
    /// Client management
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Clients
        /// <summary>
        /// Get all clients
        /// </summary>
        /// <response code="200">All clients have been returned</response>>
        /// <response code="404">No clients found</response>>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetClientDTO>>> GetClients()
        {
            var clients = await _clientService.GetClientsAsync();

            return !clients.Any()
                ? NotFound("No clients found")
                : Ok(clients);
        }

        // GET: api/Clients/5
        /// <summary>
        /// Get the client by its id
        /// </summary>
        /// <param name="id">Client id</param>
        /// <response code="200">The client has been returned</response>>
        /// <response code="404">No such client</response>>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetClientDTO>> GetClient(int id)
        {
            var client = await _clientService.GetClientAsync(id);

            return client == null 
                ? NotFound("Invalid ID. No such client found") 
                : Ok(client);
        }

        // GET: api/Client/filtered
        /// <summary>
        /// Get clients by filter
        /// </summary>
        /// <param name="filter">Filter info</param>
        /// <param name="pagination">Pagination info</param>
        /// <response code="200">Filtered client(-s) returned</response>>
        /// <response code="404">No clients found for this filter</response>>
        /// <returns></returns>
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<GetClientDTO>>> GetClientsFiltered([FromQuery] ClientFilterDTO filter, [FromQuery] PaginationDTO pagination)
        {
            var clients = await _clientService.GetClientsFilteredAsync(filter, pagination);

            return !clients.Any()
                ? NotFound("No clients found for this filter")
                : Ok(clients);
        }

        // POST: api/Clients
        /// <summary>
        /// Create a new client
        /// </summary>
        /// <param name="dto">Client info</param>
        /// <response code="201">The client has been created</response>>
        /// <response code="400">One of the fields is filled incorrectly (for the details, see the response body)</response>>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GetClientDTO>> PostClient([FromBody] PostPutClientDTO dto)
        {
            var client = await _clientService.PostClientAsync(dto);

            return CreatedAtAction(nameof(GetClient), new { id = client.id }, client);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update the client by its ID
        /// </summary>
        /// <param name="id">Client ID</param>
        /// <param name="client">Client info</param>
        /// <response code="204">The client has been updated</response>>
        /// <response code="400">The Id field must remain the same</response>>
        /// <response code="404">The client was not found</response>>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, PostPutClientDTO dto)
        {
            var client = await _clientService.PutClientAsync(id, dto);

            return client == null 
                ? NotFound("The client was not found") 
                : NoContent();
        }

        // DELETE: api/Clients/5
        /// <summary>
        /// Delete the client by its ID
        /// </summary>
        /// <param name="id">client ID</param>
        /// <response code="204">The client has been deleted</response>>
        /// <response code="404">The client was not found</response>>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _clientService.DeleteClientAsync(id);
 
            return client == false 
                ? NotFound("The client was not found") 
                : NoContent();
        }
    }
}
