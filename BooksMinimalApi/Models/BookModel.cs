namespace BooksMinimalApi.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int ReleaseYear { get; set; }
    }
}
