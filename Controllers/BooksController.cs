using BookStore.Configuration;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [EnableQuery]
    public class BooksController : ControllerBase
    {

        private readonly BookStoreContext _context;

        public BooksController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == id);
            return Ok(book);
        }

    }
}
