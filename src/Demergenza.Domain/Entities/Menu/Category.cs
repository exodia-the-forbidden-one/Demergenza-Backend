using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demergenza.Domain.Entities.Menu
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Menu> Menus { get; set; }
    }
}