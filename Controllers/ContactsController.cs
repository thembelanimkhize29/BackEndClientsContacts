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
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _contactService;


        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
            //_context.Database.EnsureCreated();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdContact = await _contactService.CreateOrLinkContactAsync(contact);

            return CreatedAtAction(nameof(CreateContact), new { id = createdContact.Id }, createdContact);
        }


        [HttpGet]
        public async Task<ActionResult> GetContacts()
        {
            var contacts = await _contactService.GetContactsAsync();
            if (contacts == null)
            {
                return NotFound();
            }
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetContact(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpGet("list-by-surname")]
        public async Task<ActionResult<List<Contact>>> GetContactsOrderedBySurname()
        {
            var contacts = await _contactService.GetAllContactsOrderedBySurNameAsync();

            if (contacts == null || contacts.Count == 0)
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        [HttpGet("count-linked-clients")]
        public async Task<ActionResult<int>> CountLinkedClients([FromQuery] string contactEmail)
        {
            if (string.IsNullOrWhiteSpace(contactEmail))
            {
                return BadRequest("Contact email is required.");
            }

            var count = await _contactService.CountLinkedClientsAsync(contactEmail);

            return Ok(count);
        }

        // [HttpPost]
        // public async Task<ActionResult> PostContact(Contact contact)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest();
        //     }
        //     _context.Contacts.Add(contact);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(
        //         nameof(GetContact),
        //          new { id = contact.Id }, contact);
        // }
    }
}