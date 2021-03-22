using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueBookLibrary.ServerApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace VueBookLibrary.ServerApp.Controllers
{
    [ApiController]
    [Route("api/books")]
    [Authorize]
    public class BookValuesController : ControllerBase
    {
        private DataContext context;

        public BookValuesController(DataContext ctx)
        {
            context = ctx;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            Book book = await context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [AllowAnonymous]
        [HttpGet]
        public IAsyncEnumerable<Book> GetBooks()
        {
            var query = context.Books;
            return query;
        }
        [HttpPost]
        public async Task<IActionResult> SaveBook([FromBody] BookData bookData)
        {
            Book book = bookData.ToBook();
            book.BookId = 0;
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return Ok(book);
        }
        [AllowAnonymous]
        [HttpPut]
        public async Task UpdateBook(Book book)
        {
            context.Update(book);
            await context.SaveChangesAsync();
        }
        [HttpDelete("{id}")]
        public async Task DeleteBook(int id)
        {
            context.Books.Remove(new Book { BookId = id });
            await context.SaveChangesAsync();
        }
    }
}
