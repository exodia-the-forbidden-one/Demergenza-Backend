namespace Demergenza.Domain.Entities.Menu
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;

        public Guid AdminId { get; set; }

        public string Image { get; set; } = null!;

        public virtual Admin.Admin Admin { get; set; } = null!;

        public virtual List<Menu> Menus { get; set; }
    }
}