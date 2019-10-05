using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductsController()
        {
            _productRepository = new ProductRepository();
        }

        // GET api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return new OkObjectResult(_productRepository.All());
        }

        // GET api/products/:id
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var Product = _productRepository.Key(id);
            if (Product != null)
            {
                return new OkObjectResult(Product);
            }
            return NotFound();
        }
    }
}