using Microsoft.AspNetCore.Http;

namespace Demergenza.Domain.Entities.Models
{
    public class AddMenuModel
    {
        public string MenuName { get; set; }
        public IFormFile? MenuImage { get; set; }
        public string AdminUserName { get; set; }
        public string MenuIngredients { get; set; }
        public int MenuPrice { get; set; }
        public string CategoryId { get; set; }
    }
}