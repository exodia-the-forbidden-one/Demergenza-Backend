
using Microsoft.AspNetCore.Http;

namespace Demergenza.Domain.Entities.Models
{
    public class AddCategoryModel
    {
        public string AdminUsername { get; set; }  = null!;
        public string CategoryName { get; set; }  = null!;
        public IFormFile categoryImage { get; set; }  = null!;
    }
}