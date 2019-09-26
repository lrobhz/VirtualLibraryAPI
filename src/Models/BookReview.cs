using System;
using Newtonsoft.Json;
using src.Convertes;

namespace VirtualLibraryApi.Models
{
    public class BookReview
    {
        public long BookId { get; set; }
        public string Review { get; set; }
        public string UserName { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime ReviewDate { get; set; }
    }
}