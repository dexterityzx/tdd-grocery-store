using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Controllers
{
    [Produces("application/json")]
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
        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        /// <response code="200">Returns a list of customers.</response>
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return new OkObjectResult(_orderRepository.All());
        }

        // GET api/orders/:id
        /// <summary>
        /// Gets an order by ID.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>An order.</returns>
        /// <response code="200">Returns a order.</response>
        /// <response code="404">Order is not found.</response>
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
        /// <summary>
        /// Gets all orders by date.
        /// </summary>
        /// <param name="date">Date of the order.</param>
        /// <returns>A list of orders of a date.</returns>
        /// <response code="200">Return a list of orders.</response>
        /// <response code="400">Not able to parse the date.</response>
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