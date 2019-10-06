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
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return new OkObjectResult(_productRepository.All());
        }

        // GET api/products/:id
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _productRepository.Key(id);
            if (product != null)
            {
                return new OkObjectResult(product);
            }
            return NotFound();
        }

        // PUT api/products/
        [HttpPut]
        public ActionResult CreateProduct(Product product)
        {
            _productRepository.Add(product);
            var result = _productRepository.Save();
            if (result == 0)
            {
                return StatusCode(500);
            }
            return Accepted();
        }

        // PATCH api/products/
        [HttpPatch]
        public ActionResult UpdateProduct(Product product)
        {
            if (!_productRepository.Exist(product.Id))
            {
                return NotFound();
            }
            _productRepository.Update(product);
            var result = _productRepository.Save();
            if (result == 0)
            {
                return StatusCode(500);
            }
            return Accepted();
        }
    }
}