using Microsoft.AspNetCore.Http;

namespace Demergenza.Application.DTOs.Menu;

public class AddMenu
{
    public string MenuName { get; set; } = null!;
    public IFormFile? MenuImage { get; set; }
    public string? AdminUserName { get; set; }
    public string? MenuIngredients { get; set; }
    public int MenuPrice { get; set; }
    public string CategoryId { get; set; } = null!;
}