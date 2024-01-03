namespace Demergenza.Application.DTOs.AboutUs;

public class AboutUsResponse
{
    public string? TextContent { get; set; }

    private string? _image;
    public string? Image
    {
        get
        {
            return string.IsNullOrEmpty(_image) ? _image : $"{Environment.GetEnvironmentVariable("API_ENDPOINT")}/data-images/{_image}";
        }
        set
        {
            _image = value;
        }
    }
}