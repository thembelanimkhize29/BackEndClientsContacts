using ClientsContactsProj.Data;
using ClientsContactsProj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsContactsProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context){
            _context=context;
            _context.Database.EnsureCreated();
        }

        // [HttpGet]
        // public string GetClient()
        // {
        //     return "okd";
        // }

        // [HttpGet]
        // public IEnumerable<Client> GetAllClients()
        // {
        //     return _context.Clients.ToArray();
        // }
        [HttpGet]
        public async Task<ActionResult> GetAllClients()
        {
            return Ok(await _context.Clients.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClientsById(int id){
            var client=await _context.Clients.FindAsync(id);
            if(client==null){
                return NotFound();
            }
            return Ok(client);
        }
    }
}