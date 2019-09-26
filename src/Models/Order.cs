using System;
using Newtonsoft.Json;
using src.Convertes;
using src.Models.Enums;

namespace src.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime OrderDate { get; set; }
        public string UserName { get; set; }
        public Basket Basket { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}