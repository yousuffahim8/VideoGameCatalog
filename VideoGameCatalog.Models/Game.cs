namespace VideoGameCatalog.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
