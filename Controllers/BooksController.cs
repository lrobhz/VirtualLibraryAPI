using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VirtualLibraryApi.Models;

namespace VirtualLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private List<Book> Books ;

        public BooksController()
        {
            Books = new List<Book>() 
            { 
                new Book() { BookId = 1, BookName = "Book1", Author = "Author1", Description = "Lorem ipsum dolor sit amet, consectetur " },
                new Book() { BookId = 2, BookName = "Book2", Author = "Author2", Description = "Lorem ipsum dolor sit amet, consectetur " },
                new Book() { BookId = 3, BookName = "Book3", Author = "Author3", Description = "Lorem ipsum dolor sit amet, consectetur " },
                new Book() { BookId = 4, BookName = "Book4", Author = "Author4", Description = "Lorem ipsum dolor sit amet, consectetur " },
                new Book() { BookId = 5, BookName = "Book5", Author = "Author5", Description = "Lorem ipsum dolor sit amet, consectetur " }
            };
        }

        [HttpGet("GetAllBooks")]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Books;
        }

        [HttpGet("GetBookById/{id}")]
        public ActionResult<Book> GetBookById(long id)
        {
            return Books.Where(b => b.BookId == id).SingleOrDefault();
        }

        [HttpGet("GetBookByName/{name}")]
        public ActionResult<IEnumerable<Book>> GetBookByName(string name)
        {
            return Books.Where(b => b.BookName.Equals(name));
        }

        [HttpGet("GetBookByAuthor/{author}")]
        public ActionResult<IEnumerable<Book>> GetBookByAuthor(string author)
        {
            return Books.Where(b => b.Author.Equals(author));
        }
    }
}