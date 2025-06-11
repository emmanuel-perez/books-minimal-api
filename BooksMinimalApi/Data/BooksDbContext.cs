using BooksMinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksMinimalApi.Data
{
    public class BooksDbContext: DbContext
    {

        private readonly IConfiguration _config;

        public BooksDbContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("Default"));
        }

        public DbSet<BookModel> Books { get; set; } 
        
    }
}
