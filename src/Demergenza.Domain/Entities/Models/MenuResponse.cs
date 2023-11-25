
namespace Demergenza.Domain.Entities.Menu.Models
{
    public class MenuResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; } = null!;

        public string? Ingredients { get; set; }

        public string? Image { get; set; }

        public decimal Price { get; set; }
    }
}

