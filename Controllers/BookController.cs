using BookStore.Configuration;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : ODataController
    {

        private readonly BookStoreContext _context;

        public BookController(BookStoreContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public ActionResult<IQueryable<Book>> Get()
        {
            var books = _context.Books;
            return Ok(books);
        }

        [EnableQuery]
        public async Task<ActionResult<Book>> Get(string key)
        {
            int id = int.Parse(key);
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == id);
            return Ok(book);
        }

    }
}
