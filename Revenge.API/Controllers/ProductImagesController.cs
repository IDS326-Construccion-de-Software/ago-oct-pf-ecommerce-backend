using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/productimage")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly RevengeDbContext _context;

        public ProductImagesController(RevengeDbContext context)
        {
            _context = context;
        }

        // GET: api/productimage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productimage>>> GetProductImages()
        {
            return await _context.Productimages
                .Include(p => p.Product)
                .ToListAsync();
        }

        // GET: api/productimage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Productimage>> GetProductImageById(Guid id)
        {
            var image = await _context.Productimages.FindAsync(id);

            if (image == null)
                return NotFound();

            return image;
        }

        // GET: api/productimage/primary/{productId}
        [HttpGet("primary/{productId}")]
        public async Task<ActionResult<Productimage>> GetPrimaryImage(Guid productId)
        {
            var primaryImage = await _context.Productimages
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.IsPrimary);

            if (primaryImage == null)
                return NotFound("El producto no tiene una imagen principal.");

            return primaryImage;
        }

        // POST: api/productimage
        [HttpPost]
        public async Task<ActionResult<Productimage>> PostProductImage(Productimage image)
        {
            var productExists = await _context.Products.AnyAsync(p => p.Id == image.ProductId);
            if (!productExists)
                return BadRequest("El producto especificado no existe.");

            // Si la imagen es principal, desmarcar las otras
            if (image.IsPrimary)
            {
                var existingPrimary = await _context.Productimages
                    .Where(pi => pi.ProductId == image.ProductId && pi.IsPrimary)
                    .ToListAsync();

                foreach (var img in existingPrimary)
                {
                    img.IsPrimary = false;
                }
            }

            _context.Productimages.Add(image);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductImageById), new { id = image.Id }, image);
        }

        // DELETE: api/productimage/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(Guid id)
        {
            var image = await _context.Productimages.FindAsync(id);
            if (image == null)
                return NotFound();

            _context.Productimages.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}