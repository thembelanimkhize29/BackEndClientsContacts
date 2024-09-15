using ClientsContactsProj.Data;
using ClientsContactsProj.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsContactsProj.Services
{
    public class ContactService
    {
        private readonly AppDbContext _context;
        private readonly ClientService _clientService;

        public ContactService(AppDbContext context, ClientService clientService)
        {
            _context = context;
            _clientService = clientService;
            _context.Database.EnsureCreated();

        }

        public async Task<Contact> CreateOrLinkContactAsync(Contact contact)
    {
       
        var existingContact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Email == contact.Email);

        if (existingContact != null)
        {
            foreach (var client in contact.Clients)
            {
                var existingClient = await _context.Clients
                    .FirstOrDefaultAsync(c => c.FirstName == client.FirstName);

                if (existingClient == null)
                {
                    client.ClientCode = _clientService.GenerateClientCode(client.FirstName);
                    existingClient = _context.Clients.Add(client).Entity;
                }

                if (!existingClient.Contacts.Contains(existingContact))
                {
                    existingClient.Contacts.Add(existingContact);
                }
            }

            await _context.SaveChangesAsync();
            return existingContact; 
        }

        // If no existing contact, create a new one
        var uniqueClients = new HashSet<Client>();
        foreach (var client in contact.Clients)
        {
            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.FirstName == client.FirstName);

            if (existingClient == null)
            {
                client.ClientCode = _clientService.GenerateClientCode(client.FirstName);
                existingClient = _context.Clients.Add(client).Entity;
            }
            uniqueClients.Add(existingClient);
        }

        contact.Clients = uniqueClients;

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        return contact;
    }
          
        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetContactByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<List<Contact>> GetAllContactsOrderedBySurNameAsync()
        {
            return await _context.Contacts
                .OrderBy(c => c.Surname)
                .ToListAsync();
        }

        public async Task<int> CountLinkedClientsAsync(string contactEmail)
        {
            
            string processedEmail = contactEmail.Trim().ToLower();

            return await _context.Contacts
                .Where(c => c.Email == processedEmail)
                .SelectMany(c => c.Clients)
                .CountAsync();
        }
    }
}
