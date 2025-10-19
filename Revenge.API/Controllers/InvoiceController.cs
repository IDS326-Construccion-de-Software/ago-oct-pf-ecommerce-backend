using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Revenge.Core.Models;
using Revenge.Data.Repositories;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{

    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository,
        IOrderRepository orderRepository,
        IAuthenticationRepository authenticationRepository
        )
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
            _authenticationRepository = authenticationRepository;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Invoice[]>> GetInvoiceByUser(Guid userId)
        {
            try
            {
                InvoiceDTO[] invoices = await _invoiceRepository.FindInvoicesByUserAsync(userId);
                if (invoices.Length < 1) return NotFound("");
                return Ok(invoices);
            }
            catch (Exception)
            {

                return StatusCode(500, "Error interno del servidor");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceById(Guid id)
        {
            try
            {
                InvoiceDTO? invoice = await _invoiceRepository.FindInvoiceByIdAsync(id);

                if (invoice == null) return NotFound("Esta factura no está registrada");

                return Ok(invoice);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoice([FromBody] CreateInvoiceDTO createInvoiceDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {

                bool validUser = await _authenticationRepository.ExistsAsync(createInvoiceDTO.UserId);

                if (!validUser) return NotFound("Usuario no registradp");

                bool validOrder = await _orderRepository.ExistsAsync(createInvoiceDTO.OrderId);

                if (!validOrder) return NotFound("Pedido no registradp");

                Invoice newInvoice = new Invoice
                {
                    Id = Guid.NewGuid(),
                    OrderId = createInvoiceDTO.OrderId,
                    UserId = createInvoiceDTO.UserId,
                    Tax = createInvoiceDTO.Tax,
                    Total = createInvoiceDTO.Total,
                    Url = createInvoiceDTO.Url,
                };
                
                await _invoiceRepository.AddInvoiceAsync(newInvoice);

                return CreatedAtAction(
                nameof(GetInvoiceById),
                new { id = newInvoice.Id },
                new { InvoiceUrl = newInvoice.Url }
                );
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            try
            {
                bool deletedInvoice = await _invoiceRepository.DeleteInvoiceAsync(id);

                if (!deletedInvoice) return NotFound("La factura a eliminar no está registrada");

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}