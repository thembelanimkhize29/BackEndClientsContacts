using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
 
namespace ClientsContactsProj.Models
{
[Table("clients")]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }=null!;

       
        // [Index(IsUnique = true)]
        public string ClientCode { get; set; }=null!;

// Many-to-many relationship with Contact
        public virtual ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();

        public void GenerateClientCode(string prefix, int number)
        {
            ClientCode = $"{prefix.ToUpper()}{number:D3}";
        }

    }
}