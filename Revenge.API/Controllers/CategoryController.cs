using Microsoft.AspNetCore.Mvc;
using Revenge.Core.Models;
using Revenge.Infrestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Revenge.API_oct_pf_ecommerce_backend.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            if (categories == null || !categories.Any())
                return NotFound("No se encontraron categorías registradas.");

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (category == null)
                return NotFound($"No se encontró ninguna categoría con el ID: {id}.");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO category, CancellationToken cancellationToken)
        {
            category.Id = Guid.NewGuid();
            var success = await _categoryRepository.AddAsync(category, cancellationToken);

            if (!success)
                return BadRequest("No se pudo crear la categoría.");

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, CategoryDTO category, CancellationToken cancellationToken)
        {
            if (id != category.Id)
                return BadRequest("El ID no coincide con la categoría enviada.");

            var exists = await _categoryRepository.ExistsAsync(id, cancellationToken);
            if (!exists)
                return NotFound($"No existe una categoría con el ID: {id}.");

            var success = await _categoryRepository.UpdateAsync(category, cancellationToken);
            if (!success)
                return BadRequest("Error al actualizar la categoría.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            var success = await _categoryRepository.DeleteAsync(id, cancellationToken);
            if (!success)
                return NotFound($"No se encontró ninguna categoría con el ID: {id} para eliminar.");

            return NoContent();
        }
    }
}
