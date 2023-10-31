using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demergenza.Domain.Entities.Models
{
    public class UpdateCategoryModel
    {
        public string AdminUsername { get; set; }
        public IFormFile? CategoryImage { get; set; }
        public string? CategoryName { get; set; }
    }
}