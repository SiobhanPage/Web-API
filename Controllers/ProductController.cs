using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository<Product> _repository;

        public ProductController(ILogger<ProductController> logger, 
                                 IProductRepository<Product> productRepository)
        {
            _logger = logger;
            _repository = productRepository;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<ProductDto>>> GetAll()
        {
            _logger.LogInformation("ActionResult GetAll");

            var items = await _repository.ListAllAsync();
            var result = items.Select(Map.ProductToDto);

            return Ok(result);
        }

        /// <summary>
        /// Finds all products matching the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductsByName([FromQuery] string name)
        {
            _logger.LogInformation("ActionResult GetProductsByName");

            var items = await _repository.SearchByNameAsync(name);
            var result = items.Select(Map.ProductToDto);

            return Ok(result);
        }

        /// <summary>
        /// Gets the product that matches the specified Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            _logger.LogInformation("ActionResult GetProduct");

            var product = await _repository.GetByIdAsync(id);

            return product == null ? NotFound() : Ok(Map.ProductToDto(product));
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto productDto)
        {
            _logger.LogInformation("ActionResult CreateProduct");

            var product = Map.DtoToProduct(productDto);
            await _repository.AddAsync(product);

            return CreatedAtAction(nameof(CreateProduct), new { id = product.Guid }, productDto);
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(Guid id, ProductDto productDto)
        {
            _logger.LogInformation("ActionResult UpdateProduct");

            if (id != productDto.Guid) return BadRequest("Id parameters do not match");
 
            var product = Map.DtoToProduct(productDto);
            await _repository.UpdateAsync(product);

            return NoContent();
        }

        /// <summary>
        /// Deletes a product and its options.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            _logger.LogInformation("ActionResult DeleteProduct");

            var product = await _repository.GetByIdAsync(id);

            if (product == null) return NotFound();
            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}


