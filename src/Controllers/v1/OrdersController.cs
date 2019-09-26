using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Models.Enums;
using VirtualLibraryApi;
using System.Collections.Generic;

namespace src.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet("{userName}")]
        public ActionResult<IEnumerable<Order>> GetOrders(string userName)
        {
            var orders = Startup.Orders.Where(o => o.UserName.Equals(userName));

            if(orders == null || orders.Count() == 0)
                return NoContent();   

            return Ok(orders);
        }

        [HttpGet("{orderId}/status")]
        public ActionResult<OrderStatus> GetOrderStatus(int orderId)
        {
             if(!Startup.Orders.Any(o => o.OrderId == orderId))
                return Conflict(new { message = "Order not found"});

            var order = Startup.Orders.Where(o => o.OrderId == orderId).SingleOrDefault();

            return order.OrderStatus;
        }

        [HttpPost("Create/{userName}/{basketId}")]
        public ActionResult Create(string userName, int basketId)
        {
            if(!Startup.Baskets.ContainsKey(userName))
                return Conflict(new { message = "User has no basket"});

            if(Startup.Baskets[userName].BasketId != basketId)
                return Conflict(new { message = "Invalid Basket Id"});

            var order = new Order
            {
                OrderId = Startup.Orders.Count + 1,
                OrderDate = DateTime.Now,
                UserName = userName,
                Basket = Startup.Baskets[userName],
                OrderStatus = OrderStatus.Created
            };

            return StatusCode(201); 
        }

        [HttpPost("Checkout/{orderId}")]
        public ActionResult Checkout(int orderId)
        {
            if(!Startup.Orders.Any(o => o.OrderId == orderId))
                return Conflict(new { message = "Order not found"});

            var order = Startup.Orders.Where(o => o.OrderId == orderId).SingleOrDefault();
            order.OrderStatus = OrderStatus.ReadyForShip;

            return Ok(); 
        }
    }
}