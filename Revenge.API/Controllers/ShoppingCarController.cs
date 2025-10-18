using Microsoft.AspNetCore.Mvc;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCarController : ControllerBase
    {
        private readonly IShoppingcartRepository _shoppingcartRepository;

        public ShoppingCarController(IShoppingcartRepository shoppingcartRepository)
        {
            _shoppingcartRepository = shoppingcartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoppingcart>>> GetAll()
        {
            var carts = await _shoppingcartRepository.GetAllAsync();
            if (carts == null || !carts.Any())
                return NotFound("No se encontraron carritos registrados.");

            return Ok(carts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shoppingcart>> GetById(Guid id)
        {
            var cart = await _shoppingcartRepository.GetByIdAsync(id);
            if (cart == null)
                return NotFound($"No se encontró ningún carrito con el ID: {id}.");

            return Ok(cart);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Shoppingcart>> GetByUserId(Guid userId)
        {
            var cart = await _shoppingcartRepository.GetByUserIdAsync(userId);
            if (cart == null)
                return NotFound($"No se encontró ningún carrito asociado al usuario con ID: {userId}.");

            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Shoppingcart cart)
        {
            cart.Id = Guid.NewGuid();
            cart.CreatedAt = DateTime.UtcNow;

            var result = await _shoppingcartRepository.AddAsync(cart);
            if (!result)
                return BadRequest("No se pudo crear el carrito.");

            return CreatedAtAction(nameof(GetById), new { id = cart.Id }, cart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Shoppingcart cart)
        {
            if (id != cart.Id)
                return BadRequest("El ID no coincide con el carrito enviado.");

            var exists = await _shoppingcartRepository.ExistsAsync(id);
            if (!exists)
                return NotFound($"No existe un carrito con el ID: {id}.");

            var result = await _shoppingcartRepository.UpdateAsync(cart);
            if (!result)
                return BadRequest("Error al actualizar el carrito.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _shoppingcartRepository.DeleteAsync(id);
            if (!result)
                return NotFound($"No se encontró ningún carrito con el ID: {id} para eliminar.");

            return NoContent();
        }
    }
}
