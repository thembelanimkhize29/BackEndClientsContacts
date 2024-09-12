using ClientsContactsProj.Data;
using ClientsContactsProj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsContactsProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public async Task<ActionResult> GetContacts()
        {
            return Ok(await _context.Contacts.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

          [HttpPost]
        public async Task<ActionResult> PostContact(Contact contact)
        {
            if(!ModelState.IsValid){
                return BadRequest();
            }
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetContact),
                 new { id = contact.Id }, contact);
        }
    }
}