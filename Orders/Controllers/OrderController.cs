using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Orders.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return new[]
            {
                new Order(2345, 12413),
                new Order(765432, 1241456),
            };
        }

        [HttpGet("{Id}")]
        public Order Get(int id)
        {
            return new Order(id, 678);
        }
    }

    public class Order
    {
        public Order(long id, decimal finalPrice)
        {
            FinalPrice = finalPrice;
            Id = id;
        }

        public decimal FinalPrice { get; }
        public long Id { get; }
    }
}