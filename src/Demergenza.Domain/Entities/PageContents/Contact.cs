namespace Demergenza.Domain.Entities.PageContents;

public class Contact : BaseEntity
{
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? SecondPhone { get; set; }
}