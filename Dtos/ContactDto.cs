using System.ComponentModel.DataAnnotations;

public class ContactDto
{
    public long Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Surname { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public ICollection<ClientDto> Contacts { get; set; } = new HashSet<ClientDto>();

}
