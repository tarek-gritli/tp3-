using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using tp3.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Movie> movies { get; set; }
    public DbSet<Genre> genres { get; set; }

    public DbSet<Customer> customers { get; set; }

    public DbSet<MembershipType> Membershiptypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey(m => m.GenreId);

        modelBuilder.Entity<Customer>()
            .HasOne(c => c.MembershipType)
            .WithMany(m => m.Customers)
            .HasForeignKey(c => c.MembershipTypeId);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Movies)
            .WithMany(m => m.Customers);

        string genreJson = File.ReadAllText("Genres.Json");
        List<Genre>? genreList = JsonSerializer.Deserialize<List<Genre>>(genreJson);

        if (genreList != null && genreList.Any())
        {
            modelBuilder.Entity<Genre>().HasData(genreList);
        }
        else
        {
            Console.WriteLine("No genres to seed.");
        }





    }
}