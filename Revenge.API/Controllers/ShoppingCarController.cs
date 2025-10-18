using Microsoft.AspNetCore.Mvc;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
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
        public async Task<ActionResult<IEnumerable<Shoppingcart>>> GetAll(CancellationToken cancellationToken)
        {
            var carts = await _shoppingcartRepository.GetAllAsync(cancellationToken);
            if (carts == null || carts.Length == 0)
                return NotFound("No se encontraron carritos registrados.");

            return Ok(carts);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Shoppingcart>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var cart = await _shoppingcartRepository.GetByIdAsync(id, cancellationToken);
            if (cart == null)
                return NotFound($"No se encontró ningún carrito con el ID: {id}.");

            return Ok(cart);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<Shoppingcart>> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var cart = await _shoppingcartRepository.GetByUserIdAsync(userId, cancellationToken);
            if (cart == null)
                return NotFound($"No se encontró ningún carrito asociado al usuario con ID: {userId}.");

            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Shoppingcart cart, CancellationToken cancellationToken)
        {
            cart.Id = Guid.NewGuid();
            cart.CreatedAt = DateTime.UtcNow;

            var result = await _shoppingcartRepository.AddAsync(cart, cancellationToken);
            if (!result)
                return BadRequest("No se pudo crear el carrito.");

            return CreatedAtAction(nameof(GetById), new { id = cart.Id }, cart);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Shoppingcart cart, CancellationToken cancellationToken)
        {
            if (id != cart.Id)
                return BadRequest("El ID no coincide con el carrito enviado.");

            var exists = await _shoppingcartRepository.ExistsAsync(id, cancellationToken);
            if (!exists)
                return NotFound($"No existe un carrito con el ID: {id}.");

            var result = await _shoppingcartRepository.UpdateAsync(cart, cancellationToken);
            if (!result)
                return BadRequest("Error al actualizar el carrito.");

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _shoppingcartRepository.DeleteAsync(id, cancellationToken);
            if (!result)
                return NotFound($"No se encontró ningún carrito con el ID: {id} para eliminar.");

            return NoContent();
        }
    }
}
