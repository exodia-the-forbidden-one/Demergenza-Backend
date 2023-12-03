using Microsoft.AspNetCore.Http;

namespace Demergenza.Application.DTOs.AboutUs;

public class SetAboutUs
{
    public string? TextContent { get; set; }
    public IFormFile? Image { get; set; }
}