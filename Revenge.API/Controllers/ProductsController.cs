using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            if (products == null || products.Length == 0)
                return NotFound("No se encontraron productos registrados.");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (product == null)
                return NotFound($"No se encontró ningún producto con el ID: {id}.");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product, CancellationToken cancellationToken)
        {
            var result = await _productRepository.AddAsync(product, cancellationToken);
            if (!result)
                return BadRequest("No se pudo crear el producto.");

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product, CancellationToken cancellationToken)
        {
            if (id != product.Id)
                return BadRequest("El ID no coincide con el producto enviado.");

            var exists = await _productRepository.ExistsAsync(id, cancellationToken);
            if (!exists)
                return NotFound($"No existe un producto con el ID: {id}.");

            var updated = await _productRepository.UpdateAsync(product, cancellationToken);
            if (!updated)
                return BadRequest("Error al actualizar el producto.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _productRepository.DeleteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound($"No se encontró ningún producto con el ID: {id} para eliminar.");

            return NoContent();
        }
    }
}
