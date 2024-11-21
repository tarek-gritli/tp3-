namespace tp3.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public DateTime DateAdded { get; set; }
        public string? PhotoPath { get; set; }


    }
}