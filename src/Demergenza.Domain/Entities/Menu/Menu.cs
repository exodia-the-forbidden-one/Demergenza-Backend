using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demergenza.Domain.Entities.Menu
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Ingredients { get; set; } = null!;

        public string Image { get; set; } = null!;

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }

        public Guid AdminId { get; set; }
        public virtual Admin.Admin Admin { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
    }
}