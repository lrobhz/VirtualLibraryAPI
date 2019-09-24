using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using VirtualLibraryApi;
using VirtualLibraryApi.Models;

namespace src.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class BasketController : ControllerBase
    {
        [HttpGet("{userName}/Books")]
        public ActionResult<IEnumerable<Book>> ListCartBooks(string userName)
        {
            if(Startup.Baskets.ContainsKey(userName))
                return Startup.Baskets[userName].Books;

            return NoContent();
        }

        [HttpPost("{userName}/AddBook/{bookId}")]
        public ActionResult AddBook(string userName, long bookId)
        {
            if(!Startup.Baskets.ContainsKey(userName))
            {
                Startup.Baskets.Add(userName, new Basket() { UserName = userName, Books = new List<Book>() });
            }

            var book = Startup.Books.Where(b => b.BookId == bookId).SingleOrDefault();

            if(book == null)
                return Conflict(new { message = $"No book with id '{bookId}' was found."});

            Startup.Baskets[userName].Books.Add(book);

            return StatusCode(201); 
        }

        [HttpPost("{userName}/RemoveBook/{bookId}")]
        public ActionResult RemoveBook(string userName, long bookId)
        {
            if(!Startup.Baskets.ContainsKey(userName))
            {
                return Conflict(new { message = $"The user {userName} has no basket."});
            }

            var book = Startup.Baskets[userName].Books.Where(b => b.BookId == bookId).SingleOrDefault();

            if(book == null)
                return Conflict(new { message = $"No book with id '{bookId}' was found on {userName}'s basket."});

            Startup.Baskets[userName].Books.Remove(book);

            return Ok(); 
        }

        [HttpPost("{userName}/ClearBasket")]
        public ActionResult ClearBasket(string userName)
        {
            if(!Startup.Baskets.ContainsKey(userName))
            {
                return Conflict(new { message = $"The user {userName} has no basket."});
            }

            Startup.Baskets[userName].Books.Clear();

            return Ok();
        }
    }
}