using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrdersController()
        {
            _orderRepository = new OrderRepository();
        }

        // GET api/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return new OkObjectResult(_orderRepository.All());
        }

        // GET api/orders/:id
        [HttpGet("{id}")]
        public ActionResult<Order> GetbyId(int id)
        {
            var order = _orderRepository.Key(id);
            if (order != null)
            {
                return new OkObjectResult(order);
            }
            return NotFound();
        }

        // GET api/orders/date/:date
        [HttpGet("date/{date}")]
        public ActionResult<Order> GetByDate(string date)
        {
            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                List<Order> orders = _orderRepository.GetByDate(parsedDate.ToString("yyyy/MM/dd")).ToList();
                if (orders.Count() > 0)
                {
                    return new OkObjectResult(orders);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}