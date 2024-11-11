using Microsoft.AspNetCore.Mvc;


namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders =
        [
            new Order { Id = 1, UserId = 1, Product = "Laptop", Quantity = 1 },
            new Order { Id = 2, UserId = 2, Product = "Phone", Quantity = 2 }
        ];

        [HttpGet]
        public IActionResult GetOrders() => Ok(_orders);

        [HttpGet("{id:int}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            order.Id = _orders.Max(o => o.Id) + 1;
            _orders.Add(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOrder(int id, Order updatedOrder)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            order.Product = updatedOrder.Product;
            order.Quantity = updatedOrder.Quantity;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            _orders.Remove(order);
            return NoContent();
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Product { get; set; }
        public int Quantity { get; set; }
    }
}