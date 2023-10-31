
using Microsoft.AspNetCore.Http;

namespace Demergenza.Domain.Entities.Models
{
    public class AddCategoryModel
    {
        public string AdminUsername { get; set; }
        public string CategoryName { get; set; }
        public IFormFile categoryImage { get; set; }
    }
}