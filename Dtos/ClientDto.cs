using System.ComponentModel.DataAnnotations;

public class ClientDto
{
    public long Id { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(6)]
    public string ClientCode { get; set; } = null!;

    public ICollection<ContactDto> Contacts { get; set; } = new HashSet<ContactDto>();
}
