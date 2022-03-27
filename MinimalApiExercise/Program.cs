using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MinimalApiExercise.Data;
using MinimalApiExercise.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=app.db"));
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/books", (AppDbContext context) =>
    {
        var books = context.Books.OrderBy(b => b.Title);
        return Results.Ok(books);
    });

app.MapGet("/books/{isbn}", async (string isbn, AppDbContext context) =>
    {
        var book = await context.Books.FirstOrDefaultAsync(b => b.Isbn == isbn);
        return Results.Ok(book);
    });

app.MapPost("/books", async (Book book, AppDbContext context, HttpRequest request) =>
{
    await context.Books.AddAsync(book);
    await context.SaveChangesAsync();
    var uri = new Uri($"{request.Scheme}://{request.Host}");
    return Results.Created(uri, "/books/{book.Isbn}");
});

app.MapPut("/books/{isbn}", async (Book book, AppDbContext context, HttpRequest request) =>
{
    context.Books.Update(book);
    await context.SaveChangesAsync();
});

app.Run();