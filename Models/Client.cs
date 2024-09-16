using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientsContactsProj.Models
{
    [Table("clients")]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string FirstName { get; set; }= null!;

        [Required]
        [Column("client_code")]
        [MaxLength(6)]
        public string ClientCode { get; set; }= null!;

        // many-to-many relationship with Contact
        public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    }
}
