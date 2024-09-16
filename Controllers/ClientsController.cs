using ClientsContactsProj.Data;
using ClientsContactsProj.Models;
using ClientsContactsProj.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsContactsProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
//            _context.Database.EnsureCreated();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest();
            // }
            client.ClientCode = _clientService.GenerateClientCode(client.FirstName);

            var createdClient = await _clientService.CreateClientAsync(client);

            return CreatedAtAction(nameof(CreateClient), new { id = createdClient.Id }, createdClient);
        }


        [HttpGet]
        public async Task<ActionResult> GetClients()
        {
            var clients = await _clientService.GetClientsAsync();
            if(clients==null){
                return NotFound();
            }
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Client>>> GetClientsByFirstName()
        {
            var clients = await _clientService.GetAllClientsOrderedByNameAsync();

            if (clients == null || clients.Count == 0)
            {
                return NotFound(); 
            }

            return Ok(clients);
        }

        [HttpGet("count-linked-contacts")]
        public async Task<ActionResult<int>> CountLinkedContacts([FromQuery] string clientName)
        {
            //check if we have that client
            
            var count = await _clientService.CountLinkedContactsAsync(clientName);

            return Ok(count);
        }

        // [HttpGet]
        // public string GetClient()
        // {
        //     return "ok";
        // }

        // [HttpGet]
        // public IEnumerable<Client> GetAllClients()
        // {
        //     return _context.Clients.ToArray();
        // }
      
    }
}