using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ClientsContactsProj.Models
{
    [Table("contacts")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }= null!;

        [Required]
        [Column("surname")]
        public string Surname { get; set; }= null!;

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }= null!;

        // many-to-many relationship with Client
        [JsonIgnore]
        public ICollection<Client> Clients { get; set; } = new HashSet<Client>();

    }
}
