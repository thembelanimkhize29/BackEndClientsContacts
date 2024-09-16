using ClientsContactsProj.Data;
using ClientsContactsProj.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientsContactsProj.Services
{
    public class ClientService
    {
        private readonly AppDbContext _context;

        public ClientService(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();

        }


        public string GenerateClientCode(string name)
        {
            string alphaPart = GenerateAlphaPart(name);
            int numericPart = 1; // Start with 001

            string clientCode;
            do
            {
                clientCode = alphaPart + string.Format("{0:D3}", numericPart);
                numericPart++;
            } while (_context.Clients.Any(c => c.ClientCode == clientCode));

            return clientCode;
        }

        private string GenerateAlphaPart(string name)
        {

            var words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var alphaPart = new string(words.Select(w => w[0]).ToArray()).ToUpper();

            if (alphaPart.Length < 3)
            {
                alphaPart = alphaPart.PadRight(3, 'A');
            }
            else if (alphaPart.Length > 3)
            {
                alphaPart = alphaPart.Substring(0, 3);
            }

            return alphaPart;
        }


        public async Task<Client> CreateClientAsync(Client client)
        {
            var uniqueContacts = new HashSet<Contact>();

            foreach (var contact in client.Contacts)
            {
                var existingContact = await _context.Contacts
                    .FirstOrDefaultAsync(c => c.Email == contact.Email);

                if (existingContact == null)
                {
                    _context.Contacts.Add(contact);
                    uniqueContacts.Add(contact);
                }
                else
                {
                    uniqueContacts.Add(existingContact);
                }
            }

            client.Contacts = uniqueContacts;
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            return await _context.Clients
                .Include(c => c.Contacts)
                .ToListAsync();
        }


        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<List<Client>> GetAllClientsOrderedByNameAsync()
        {
            return await _context.Clients
            .Include(c => c.Contacts)
                .OrderBy(c => c.FirstName)
                .ToListAsync();
        }

        public async Task<int> CountLinkedContactsAsync(string clientName)
        {
            string processedName = clientName.Trim();

            return await _context.Clients
                .Where(c => c.FirstName == processedName)
                .SelectMany(c => c.Contacts)
                .CountAsync();
        }
    }
}