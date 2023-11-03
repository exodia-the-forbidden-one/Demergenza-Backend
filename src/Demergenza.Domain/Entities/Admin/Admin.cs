using Demergenza.Domain.Entities.Menu;

namespace Demergenza.Domain.Entities.Admin
{
    public class Admin : BaseEntity
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public virtual ICollection<Category> TblCategories { get; set; } = new List<Category>();

        public virtual ICollection<Menu.Menu> TblMenus { get; set; } = new List<Menu.Menu>();
    }
}