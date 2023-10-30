
using Microsoft.AspNetCore.Http;

namespace Demergenza.Domain.Entities.Models
{
    public class AddCategoryModel
    {
        public string adminUserName { get; set; }
        public string addCategoryName { get; set; }
        public IFormFile categoryImage { get; set; }
    }
}