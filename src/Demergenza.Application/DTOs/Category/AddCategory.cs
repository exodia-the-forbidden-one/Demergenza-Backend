using Microsoft.AspNetCore.Http;

namespace Demergenza.Application.DTOs.Category;

public class AddCategory
{
    public string AdminUsername { get; set; }  = null!;
    public string CategoryName { get; set; }  = null!;
    public IFormFile categoryImage { get; set; }  = null!;
}