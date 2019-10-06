using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Controllers
{
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
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return new OkObjectResult(_customerRepository.All());
        }

        // GET api/customers/:id
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _customerRepository.Key(id);
            if (customer != null)
            {
                return new OkObjectResult(customer);
            }
            return NotFound();
        }

        // GET api/customers/:id/orders
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
        [HttpPut]
        public ActionResult<int> CreateCustomer(Customer customer)
        {
            _customerRepository.Add(customer);
            var result = _customerRepository.Save();
            if (result == 0)
            {
                return StatusCode(500);
            }
            return Accepted();
        }

        // PATCH api/customers/
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
            return Accepted();
        }
    }
}