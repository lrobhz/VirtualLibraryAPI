using System.Collections.Generic;
using VirtualLibraryApi.Models;

namespace src.Models
{
    public class Basket
    {
        public string UserName { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}