using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Configuration
{
    public class BookStoreContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Press> Presses { get; set; }

        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

    }
}
