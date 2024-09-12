using ClientsContactsProj.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientsContactsProj.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Contacts)
                .WithMany(c => c.Clients)
                .UsingEntity(j => j.ToTable("client_contact_link"));
        }

        public DbSet<Client>? Clients { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
    }
}
