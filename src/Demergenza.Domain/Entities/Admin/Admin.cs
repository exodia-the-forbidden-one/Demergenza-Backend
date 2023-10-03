using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Demergenza.Domain.Entities.Menu.Admin
{
    public class Admin : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public List<Category> Categories { get; set; }
        public List<Category> Menus { get; set; }
        
    }
}