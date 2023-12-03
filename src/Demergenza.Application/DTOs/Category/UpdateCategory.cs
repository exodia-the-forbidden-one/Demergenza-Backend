using Microsoft.AspNetCore.Http;

namespace Demergenza.Application.DTOs.Category;

public class UpdateCategory
{
    public string? AdminUsername { get; set; }
    public IFormFile? CategoryImage { get; set; }
    public string? CategoryName { get; set; }
}