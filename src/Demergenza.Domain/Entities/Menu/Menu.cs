using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demergenza.Domain.Entities.Menu
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AdminId { get; set; }

        public Category Category { get; set; }
        public Admin.Admin Admin { get; set; }
    }
}