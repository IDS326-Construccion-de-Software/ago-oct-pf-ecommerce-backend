using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

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

        //  GET: api/productimage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productimage>>> GetProductImages(CancellationToken cancellationToken)
        {
            var images = await _repository.GetAllAsync(cancellationToken);
            return Ok(images);
        }

        //  GET: api/productimage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Productimage>> GetProductImageById(Guid id, CancellationToken cancellationToken)
        {
            var image = await _repository.GetByIdAsync(id, cancellationToken);
            if (image == null)
                return NotFound();

            return Ok(image);
        }

        //  GET: api/productimage/primary/{productId}
        [HttpGet("primary/{productId}")]
        public async Task<ActionResult<Productimage>> GetPrimaryImage(Guid productId, CancellationToken cancellationToken)
        {
            var primaryImage = await _repository.GetPrimaryByProductIdAsync(productId, cancellationToken);

            if (primaryImage == null)
                return NotFound("El producto no tiene una imagen principal.");

            return Ok(primaryImage);
        }

        //  POST: api/productimage
        [HttpPost]
        public async Task<ActionResult<Productimage>> PostProductImage(Productimage image, CancellationToken cancellationToken)
        {
            try
            {
                var createdImage = await _repository.AddAsync(image, cancellationToken);
                return CreatedAtAction(nameof(GetProductImageById), new { id = createdImage.Id }, createdImage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //  DELETE: api/productimage/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
