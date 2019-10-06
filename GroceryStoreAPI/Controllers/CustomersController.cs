using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;
        private readonly OrderRepository _orderRepository;

        public CustomersController()
        {
            _customerRepository = new CustomerRepository();
            _orderRepository = new OrderRepository();
        }

        // GET api/customers
        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        /// <response code="200">Returns a list of customers.</response>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return new OkObjectResult(_customerRepository.All());
        }

        // GET api/customers/:id
        /// <summary>
        /// Gets a customer by ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>A customer.</returns>
        /// <response code="200">Returns a customer.</response>
        /// <response code="404">Customer is not found.</response>
        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = _customerRepository.Key(id);
            if (customer != null)
            {
                return new OkObjectResult(customer);
            }
            return NotFound();
        }

        // GET api/customers/:id/orders
        /// <summary>
        /// Gets all orders from a cutomer.
        /// </summary>
        /// <param name="id">customer ID</param>
        /// <returns>A list of orders of a customer.</returns>
        /// <response code="200">Return a list of orders.</response>
        /// <response code="404">The customer is not found or orders are not found.</response>
        [HttpGet("{id}/orders")]
        public ActionResult<IEnumerable<Order>> GetOrders(int id)
        {
            var customer = _customerRepository.Key(id);
            if (customer == null)
            {
                return NotFound();
            }

            var orders = _orderRepository.GetByCustomerId(customer.Id).ToList();
            if (orders.Count() == 0)
            {
                return NotFound();
            }

            return new OkObjectResult(orders);
        }

        // PUT api/customers/
        /// <summary>
        /// Adds a customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/customers
        ///     {
        ///        "name": "John Doe",
        ///     }
        ///
        /// The id attribute will be ignored.
        /// </remarks>
        /// <param name="customer">An object with customer attributes.</param>
        /// <returns>New Customer ID</returns>
        /// <response code="200">A customer is created.</response>
        /// <response code="500">Fails to creat a customer.</response>
        [HttpPut]
        public ActionResult<int> AddCustomer(Customer customer)
        {
            _customerRepository.Add(customer);
            var result = _customerRepository.Save();
            if (result == 0)
            {
                return StatusCode(500);
            }
            return new OkObjectResult(customer.Id);
        }

        // PATCH api/customers/
        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/customers
        ///     {
        ///         "id" : 1
        ///         "name": "John Doe",
        ///     }
        ///
        /// </remarks>
        /// <param name="customer">An object with customer attributes.</param>
        /// <returns></returns>
        /// <response code="200">A customer is udpated.</response>
        /// <response code="500">Fails to creat a customer.</response>
        [HttpPatch]
        public ActionResult<int> UpdateCustomer(Customer customer)
        {
            if (!_customerRepository.Exist(customer.Id))
            {
                return NotFound();
            }
            _customerRepository.Update(customer);
            var result = _customerRepository.Save();
            if (result == 0)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}