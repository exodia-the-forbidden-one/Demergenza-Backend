using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Demergenza.Application.DTOs.Menu;
using Demergenza.Domain.Entities.Admin;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;

namespace Demergenza.Application.DTOs.Category
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        private string? _image;

        public string? Image
        {
            get
            {
                return string.IsNullOrEmpty(_image) ? _image : $"{Environment.GetEnvironmentVariable("API_ENDPOINT")}/data-images/{_image}";
            }
            set { _image = value; }
        }

        public virtual ICollection<MenuResponse> Menus { get; set; } = new List<MenuResponse>();

        public virtual Admin Admin { get; set; } = null!;

    }
}