using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Models;

namespace MinimalApiExercise.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn);
            entity.Property(e => e.Isbn).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.ShortDescription).HasMaxLength(500);
            entity.Property(e => e.PageCount);
            entity.Property(e => e.ReleaseDate);

            entity.HasData(new Book()
            {
                Isbn = "978-0137081073",
                Title = "The Clean Coder",
                Author = "Robert C. Martin",
                ShortDescription =
                    "In The Clean Coder: A Code of Conduct for Professional Programmers, legendary software expert Robert C. Martin introduces the disciplines, techniques, tools, and practices of true software craftsmanship. This book is packed with practical advice–about everything from estimating and coding to refactoring and testing. It covers much more than technique: It is about attitude. Martin shows how to approach software development with honor, self-respect, and pride; work well and work clean; communicate and estimate faithfully; face difficult decisions with clarity and honesty; and understand that deep knowledge comes with a responsibility to act.",
                PageCount = 242,
                ReleaseDate = new DateTime(2011, 3, 13)
            });
        });
    }
}