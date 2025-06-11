using BooksMinimalApi.Data;
using BooksMinimalApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//  ***** adding db context;
builder.Services.AddDbContext<BooksDbContext>();

var app = builder.Build();

//  ***** auto migrate DB (DEV)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BooksDbContext>();
    db.Database.Migrate();
}

//  ***** ROUTES *****

app.MapGet("/api/books", async (BooksDbContext _context) =>
{
    try
    {
        IEnumerable<BookModel> books = await _context.Books.Where(book => book.Active == true).ToListAsync<BookModel>();
        return Results.Ok(books);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/books/{id}", async (int id, BooksDbContext _context) =>
{
    try
    {
        return await _context.Books.FindAsync(id) is BookModel book ? Results.Ok(book) : Results.NotFound();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("/api/books", async (BookModel bookToAdd, BooksDbContext _context) =>
{
    try
    {
        await _context.Books.AddAsync(new BookModel { Id = 0, Title = bookToAdd.Title, Author = bookToAdd.Author, ReleaseYear = bookToAdd.ReleaseYear });
        _context.SaveChangesAsync();
        return Results.Ok(bookToAdd);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPut("/api/books", async (BookModel _updatedBook, BooksDbContext _context) => {
    try
    {
        var bookOnDb = await _context.Books.FindAsync(_updatedBook.Id);
        if (bookOnDb is null)
        {
            return Results.NotFound("Book could not be found");
        }
        bookOnDb.Title = _updatedBook.Title;
        bookOnDb.Author = _updatedBook.Author;
        bookOnDb.ReleaseYear = _updatedBook.ReleaseYear;
        await _context.SaveChangesAsync();
        return Results.Ok(bookOnDb);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapDelete("/api/books/{id}", async (int id, BooksDbContext _context) => {
    try
    {
        BookModel book = await _context.Books.FindAsync(id);
        if (book is null) return Results.NotFound();
        book.Active = false;
        _context.SaveChangesAsync();
        return Results.Ok(book);    
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();
