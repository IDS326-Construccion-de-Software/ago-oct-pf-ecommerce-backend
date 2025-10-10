using Microsoft.AspNetCore.Mvc;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/cart-items")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository _repository;

        public CartItemController(ICartItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cartitem>>> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Cartitem>> GetById(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpGet("cart/{cartId:guid}")]
        public async Task<ActionResult<IEnumerable<Cartitem>>> GetByCartId(Guid cartId)
        {
            var items = await _repository.GetByCartIdAsync(cartId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Cartitem item)
        {
            item.Id = Guid.NewGuid();
            item.AddedAt = DateTime.UtcNow;

            var result = await _repository.AddAsync(item);
            return result ? Ok(item) : BadRequest("Error creating cart item.");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Cartitem item)
        {
            if (id != item.Id) return BadRequest();

            var result = await _repository.UpdateAsync(item);
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
