using Microsoft.AspNetCore.Mvc;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            return Ok(products);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product, CancellationToken cancellationToken)
        {
            if (id != product.id)
                return BadRequest("El ID de la URL no coincide con el del producto.");

            var exists = await _productRepository.ExistsAsync(id, cancellationToken);
            if (!exists)
                return NotFound();

            var updated = await _productRepository.UpdateAsync(product, cancellationToken);

            if (!updated)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el producto.");

            return NoContent();
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product, CancellationToken cancellationToken)
        {
            var added = await _productRepository.AddAsync(product, cancellationToken);

            if (!added)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto.");

            return CreatedAtAction(nameof(GetProduct), new { id = product.id }, product);
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _productRepository.DeleteAsync(id, cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
