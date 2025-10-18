using Microsoft.AspNetCore.Mvc;
using Revenge.Core.Models;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Revenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageRepository _repository;

        public ProductImagesController(IProductImageRepository repository)
        {
            _repository = repository;
        }

        //  GET: api/productimages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImageDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var images = await _repository.GetAllAsync(cancellationToken);
            return Ok(images);
        }

        //  GET: api/productimages/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImageDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var image = await _repository.GetByIdAsync(id, cancellationToken);
            if (image == null)
                return NotFound(new { message = "La imagen no fue encontrada." });

            return Ok(image);
        }

        //  GET: api/productimages/primary/{productId}
        [HttpGet("primary/{productId}")]
        public async Task<ActionResult<ProductImageDTO>> GetPrimaryByProductId(Guid productId, CancellationToken cancellationToken)
        {
            var image = await _repository.GetPrimaryByProductIdAsync(productId, cancellationToken);
            if (image == null)
                return NotFound(new { message = "No se encontró una imagen principal para este producto." });

            return Ok(image);
        }

        //  POST: api/productimages
        [HttpPost]
        public async Task<ActionResult<ProductImageDTO>> Add(ProductImageDTO dto, CancellationToken cancellationToken)
        {
            if (dto == null)
                return BadRequest(new { message = "Los datos de la imagen son requeridos." });

            try
            {
                var added = await _repository.AddAsync(dto, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = added.Id }, added);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //  DELETE: api/productimages/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound(new { message = "La imagen no existe o ya fue eliminada." });

            return NoContent();
        }
    }
}
