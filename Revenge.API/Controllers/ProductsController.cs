using Microsoft.AspNetCore.Mvc;
using Revenge.Core.Models;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ProductDTO[]>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepository.FindAllAsync(cancellationToken);
                if (products.Length == 0)
                    return NotFound("No se encontraron productos registrados.");

                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.FindByIdAsync(id, cancellationToken);
                if (product == null)
                    return NotFound($"No se encontró ningún producto con el ID: {id}.");

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product newProduct, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var categoryExists = await _categoryRepository.ExistsAsync(newProduct.CategoryId, cancellationToken);
                if (!categoryExists)
                    return NotFound("La categoría asociada no existe.");

                newProduct.Id = Guid.NewGuid();
                newProduct.CreatedAt = DateTime.UtcNow;

                await _productRepository.AddAsync(newProduct, cancellationToken);

                return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product updatedProduct, CancellationToken cancellationToken)
        {
            if (id != updatedProduct.Id)
                return BadRequest("El ID no coincide con el producto enviado.");

            try
            {
                var exists = await _productRepository.ExistsAsync(id, cancellationToken);
                if (!exists)
                    return NotFound("El producto no existe.");

                await _productRepository.UpdateAsync(updatedProduct, cancellationToken);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _productRepository.DeleteAsync(id, cancellationToken);
                if (!deleted)
                    return NotFound("No se encontró ningún producto con el ID indicado para eliminar.");

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
