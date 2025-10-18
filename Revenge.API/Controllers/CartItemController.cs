using Microsoft.AspNetCore.Mvc;
using Revenge.Core.Models;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/cart-items")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemController(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cartitem>>> GetAll()
        {
            var items = await _cartItemRepository.GetAllAsync();
            if (items == null || !items.Any())
                return NotFound("No se encontraron ítems en el carrito.");

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cartitem>> GetById(Guid id)
        {
            var item = await _cartItemRepository.GetByIdAsync(id);
            if (item == null)
                return NotFound($"No se encontró ningún ítem con el ID: {id}.");

            return Ok(item);
        }

        [HttpGet("cart/{cartId}")]
        public async Task<ActionResult<IEnumerable<Cartitem>>> GetByCartId(Guid cartId)
        {
            var items = await _cartItemRepository.GetByCartIdAsync(cartId);
            if (items == null || !items.Any())
                return NotFound($"No se encontraron ítems asociados al carrito con ID: {cartId}.");

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CartItemDTO item)
        {
            item.Id = Guid.NewGuid();
            item.AddedAt = DateTime.UtcNow;

            var result = await _cartItemRepository.AddAsync(item);
            if (!result)
                return BadRequest("No se pudo crear el ítem en el carrito.");

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CartItemDTO item)
        {
            if (id != item.Id)
                return BadRequest("El ID no coincide con el ítem enviado.");

            var exists = await _cartItemRepository.ExistsAsync(id);
            if (!exists)
                return NotFound($"No existe un ítem con el ID: {id}.");

            var result = await _cartItemRepository.UpdateAsync(item);
            if (!result)
                return BadRequest("Error al actualizar el ítem del carrito.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _cartItemRepository.DeleteAsync(id);
            if (!result)
                return NotFound($"No se encontró ningún ítem con el ID: {id} para eliminar.");

            return NoContent();
        }
    }
}
