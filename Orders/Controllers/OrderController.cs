using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Orders.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {
        private readonly Faker<Order> _testOrders;

        public OrderController()
        {
            int ids = 0;
            _testOrders = new Faker<Order>()
                .StrictMode(true)
                .RuleFor(o => o.Id, f => ids++)
                .RuleFor(o => o.FinalPrice, f => f.Random.Decimal());
        }
        
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return new[]
            {
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate(),
                _testOrders.Generate()
            };
        }

        [HttpGet("{Id}")]
        public Order Get(int id)
        {
            var item = _testOrders.Generate();
            item.Id = id;
            return item;
        }
    }

    public class Order
    {
        public Order()
        {
        }
        public Order(long id, decimal finalPrice)
        {
            FinalPrice = finalPrice;
            Id = id;
        }

        public decimal FinalPrice { get; set;}
        public long Id { get; set; }
    }
}