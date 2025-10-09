using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            if (orders == null || orders.Length == 0)
                return NotFound("No se encontraron órdenes registradas.");

            return Ok(orders);
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound($"No se encontró ninguna orden con el ID: {id}.");

            return Ok(order);
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
                return BadRequest("El ID no coincide con la orden enviada.");

            var updated = await _orderRepository.UpdateAsync(order);
            if (!updated)
                return NotFound($"No existe una orden con el ID: {id} para actualizar.");

            return NoContent();
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            order.PlacedAt = DateTime.UtcNow;
            var added = await _orderRepository.AddAsync(order);

            if (!added)
                return BadRequest("No se pudo crear la orden.");

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var deleted = await _orderRepository.DeleteAsync(id);
            if (!deleted)
                return NotFound($"No se encontró ninguna orden con el ID: {id} para eliminar.");

            return NoContent();
        }
    }
}
