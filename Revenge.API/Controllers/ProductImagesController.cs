using Microsoft.AspNetCore.Mvc;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/productimage")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageRepository _repository;

        public ProductImagesController(IProductImageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productimage>>> GetProductImages(CancellationToken cancellationToken)
        {
            var images = await _repository.GetAllAsync(cancellationToken);
            if (images == null || images.Any())
                return NotFound("No se encontraron imágenes registradas.");

            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Productimage>> GetProductImageById(Guid id, CancellationToken cancellationToken)
        {
            var image = await _repository.GetByIdAsync(id, cancellationToken);
            if (image == null)
                return NotFound($"No se encontró ninguna imagen con el ID: {id}.");

            return Ok(image);
        }

        [HttpGet("primary/{productId}")]
        public async Task<ActionResult<Productimage>> GetPrimaryImage(Guid productId, CancellationToken cancellationToken)
        {
            var primaryImage = await _repository.GetPrimaryByProductIdAsync(productId, cancellationToken);
            if (primaryImage == null)
                return NotFound("El producto no tiene una imagen principal.");

            return Ok(primaryImage);
        }

        [HttpPost]
        public async Task<ActionResult<Productimage>> PostProductImage(Productimage image, CancellationToken cancellationToken)
        {
            try
            {
                var created = await _repository.AddAsync(image, cancellationToken);
                if (created == null)
                    return BadRequest("No se pudo crear la imagen del producto.");

                return CreatedAtAction(nameof(GetProductImageById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound($"No se encontró ninguna imagen con el ID: {id} para eliminar.");

            return NoContent();
        }
    }
}
