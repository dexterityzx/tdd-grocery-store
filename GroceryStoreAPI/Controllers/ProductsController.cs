using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GroceryStoreAPI.Controllers
{
    [Produces("application/json")]
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
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        /// <response code="200">Returns a list of products.</response>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return new OkObjectResult(_productRepository.All());
        }

        // GET api/products/:id
        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>A product.</returns>
        /// <response code="200">Returns a product.</response>
        /// <response code="404">Product is not found.</response>
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
        /// <summary>
        /// Adds a product.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/products
        ///     {
        ///         "description": "Mango",
        ///         "price": 0.5
        ///     }
        ///
        /// The id attribute will be ignored.
        /// </remarks>
        /// <param name="product">An object with product attributes.</param>
        /// <returns>New product ID</returns>
        /// <response code="200">A product is created.</response>
        /// <response code="500">Fails to creat a product.</response>
        [HttpPut]
        public ActionResult<int> AddProduct(Product product)
        {
            _productRepository.Add(product);
            var result = _productRepository.Save();
            if (result == 0)
            {
                return StatusCode(500);
            }
            return new OkObjectResult(product.Id);
        }

        // PATCH api/products/
        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/products
        ///     {
        ///         "id" : 1
        ///         "description": "Mango",
        ///         "price": 0.5
        ///     }
        ///
        /// </remarks>
        /// <param name="product">An object with product attributes.</param>
        /// <returns></returns>
        /// <response code="200">A product is udpated.</response>
        /// <response code="500">Fails to creat a product.</response>
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
            return Ok();
        }
    }
}