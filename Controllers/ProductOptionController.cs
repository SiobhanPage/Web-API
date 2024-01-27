using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("products/{productId}/options")]
    public class ProductOptionController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductOptionRepository<ProductOption> _repository;

        public ProductOptionController(ILogger<ProductController> logger, 
                                       IProductOptionRepository<ProductOption> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Finds all options for a specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IList<ProductOptionDto>>> GetOptions(Guid productId)
        {
            _logger.LogInformation("ActionResult GetOptions");

            var items = await _repository.SearchByProductIdAsync(productId);
            var result = items.Select(Map.ProductOptionToDto);

            return Ok(result);
        }

        /// <summary>
        /// Finds the specified product option for the specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOptionDto>> GetOption(Guid productId, Guid id)
        {
            _logger.LogInformation("ActionResult GetOption");

            var option = await _repository.GetByProductAndOptionIdAsync(productId, id);

            return option == null ? NotFound() : Ok(Map.ProductOptionToDto(option));
        }

        /// <summary>
        /// Adds a new product option to the specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="optionDto"></param>
        /// <returns></returns>
        [HttpPost]     
        public async Task<ActionResult<ProductOptionDto>> CreateOption(Guid productId, ProductOptionCreateDto optionDto)
        {
            _logger.LogInformation("ActionResult CreateOption");

            var option = Map.CreateDtoToProductOption(productId, optionDto);

            await _repository.AddAsync(option);

            return CreatedAtAction(nameof(CreateOption), new { id = option.Guid }, optionDto);
        }

        /// <summary>
        /// Updates the specified product option.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="optionFields"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOption(Guid id, ProductOptionFieldsOnlyDto optionFields)
        {
            _logger.LogInformation("ActionResult UpdateOption");

            // Ensure the option exists before updating
            var option = await _repository.GetByIdAsync(id);

            if (option == null) return NotFound();

            option.Name = optionFields.Name;
            option.Description = optionFields.Description;

            await _repository.UpdateAsync(option);

            return NoContent();
        }

        /// <summary>
        /// Deletes the specified product option.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption(Guid id)
        {
            _logger.LogInformation("ActionResult DeleteOption");

            var option = await _repository.GetByIdAsync(id);

            if (option == null) return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
