namespace Demergenza.Domain.Entities.PageContents;

public class Hero : BaseEntity
{
    public string ImageSrc { get; set; }
    public string? ImageWidth { get; set; }
    public string? Alt { get; set; }
    public string? Title { get; set; }
    public string? TitleSrc { get; set; }
    public string? Subtitle { get; set; }
    public string? Top { get; set; }
}