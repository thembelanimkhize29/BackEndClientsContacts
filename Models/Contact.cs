using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClientsContactsProj.Models
{
    [Table("contacts")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        
        public string Name { get; set; } = null!;

       
        public string Surname { get; set; } = null!;

        
        [EmailAddress]
        // Validate as email format
        public string Email { get; set; } = null!;

        // Many-to-many relationship with Client
        public virtual ICollection<Client> Clients { get; set; } = new HashSet<Client>();
    }
}