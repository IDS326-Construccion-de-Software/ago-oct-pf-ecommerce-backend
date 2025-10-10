using Microsoft.AspNetCore.Mvc;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingcartController : ControllerBase
    {
        private readonly IShoppingcartRepository _repository;

        public ShoppingcartController(IShoppingcartRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoppingcart>>> GetAll()
        {
            var carts = await _repository.GetAllAsync();
            return Ok(carts);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Shoppingcart>> GetById(Guid id)
        {
            var cart = await _repository.GetByIdAsync(id);
            return cart == null ? NotFound() : Ok(cart);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<Shoppingcart>> GetByUserId(Guid userId)
        {
            var cart = await _repository.GetByUserIdAsync(userId);
            return cart == null ? NotFound() : Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Shoppingcart cart)
        {
            cart.Id = Guid.NewGuid();
            cart.CreatedAt = DateTime.UtcNow;

            var result = await _repository.AddAsync(cart);
            return result ? Ok(cart) : BadRequest("Error creating cart.");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Shoppingcart cart)
        {
            if (id != cart.Id) return BadRequest();

            var result = await _repository.UpdateAsync(cart);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _repository.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
