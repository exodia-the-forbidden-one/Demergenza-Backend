using Microsoft.AspNetCore.Http;

namespace Demergenza.Domain.Entities.Models;

public class UpdateMenuModel
{
    public string Id { get; set; } = null!;
    public string MenuName { get; set; } = null!;
    public IFormFile? MenuImage { get; set; }
    public string? AdminUsername { get; set; }
    public string? MenuIngredients { get; set; }
    public int MenuPrice { get; set; }
}