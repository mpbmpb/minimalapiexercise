namespace MinimalApiExercise.Models;

public class Book
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ShortDescription { get; set; }
    public int PageCount { get; set; }
    public DateTime ReleaseDate { get; set; }
}