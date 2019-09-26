using System.Collections.Generic;
using VirtualLibraryApi.Models;

namespace src.Models
{
    public class Basket
    {
        public int BasketId { get; set; }
        public string UserName { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public double Total { get; set; }
    }
}