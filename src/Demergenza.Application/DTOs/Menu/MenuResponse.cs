namespace Demergenza.Application.DTOs.Menu;

public class MenuResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; } = null!;

    public string? Ingredients { get; set; }

    private string? _image;

    public string? Image
    {
        get
        {
            return string.IsNullOrEmpty(_image) ? _image : $"{Environment.GetEnvironmentVariable("API_ENDPOINT")}/data-images/{_image}";
        }
        set { _image = value; }
    }

    public decimal Price { get; set; }
}