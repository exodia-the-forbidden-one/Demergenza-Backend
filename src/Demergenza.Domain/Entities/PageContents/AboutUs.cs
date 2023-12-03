namespace Demergenza.Domain.Entities.PageContents;

public class AboutUs : BaseEntity
{
    public Guid Id { get; set; }
    public string? TextContent { get; set; }
    public string? ImagePath { get; set; }
}