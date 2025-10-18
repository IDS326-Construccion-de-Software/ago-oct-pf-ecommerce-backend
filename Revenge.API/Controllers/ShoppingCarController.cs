using Microsoft.AspNetCore.Mvc;
using Revenge.Core.Models;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingcartRepository _shoppingcartRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public ShoppingCartController(
            IShoppingcartRepository shoppingcartRepository,
            IAuthenticationRepository authenticationRepository)
        {
            _shoppingcartRepository = shoppingcartRepository;
            _authenticationRepository = authenticationRepository;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ShoppingCartDTO[]>> GetCartsByUser(Guid userId)
        {
            try
            {
                var carts = await _shoppingcartRepository.FindCartsByUserAsync(userId);
                if (carts.Length < 1) return NotFound("No se encontraron carritos para este usuario.");
                return Ok(carts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartDTO>> GetCartById(Guid id)
        {
            try
            {
                var cart = await _shoppingcartRepository.FindCartByIdAsync(id);
                if (cart == null) return NotFound("Carrito no encontrado.");
                return Ok(cart);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] Shoppingcart cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                bool validUser = await _authenticationRepository.ExistsAsync(cart.UserId);
                if (!validUser) return NotFound("Usuario no registrado.");

                cart.Id = Guid.NewGuid();
                cart.CreatedAt = DateTime.UtcNow;

                await _shoppingcartRepository.AddCartAsync(cart);

                return CreatedAtAction(nameof(GetCartById), new { id = cart.Id }, new { CartId = cart.Id });
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(Guid id, [FromBody] Shoppingcart cart)
        {
            if (id != cart.Id)
                return BadRequest("El ID no coincide con el carrito enviado.");

            try
            {
                var exists = await _shoppingcartRepository.ExistsAsync(id);
                if (!exists)
                    return NotFound("El carrito no existe.");

                await _shoppingcartRepository.UpdateCartAsync(cart);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            try
            {
                bool deleted = await _shoppingcartRepository.DeleteCartAsync(id);
                if (!deleted) return NotFound("Carrito no encontrado para eliminar.");
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
