using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;
using Revenge.Infrestructure.Entities;
using Revenge.Infrestructure.Repositories;

namespace Revenge.Data.Repositories
{
    
    /// Repositorio para la entidad Product.
    /// Implementa la interfaz IProductRepository
    /// y maneja operaciones CRUD sobre la tabla Products.
   
    public class ProductRepository : IProductRepository
    {
        private readonly RevengeDbContext _context;

        public ProductRepository(RevengeDbContext context)
        {
            _context = context;
        }

        
        /// Obtiene todos los productos, incluyendo su categoría e imágenes asociadas.
        
        public async Task<Product[]?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(p => p.Category)        // Incluye la relación con categoría
                .Include(p => p.Productimages)   // Incluye imágenes del producto
                .ToArrayAsync(cancellationToken);
        }

       
        /// Busca un producto por su ID, con categoría e imágenes relacionadas.
        
        public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Productimages)
                .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }

      
        /// Agrega un nuevo producto a la base de datos.
        
        public async Task<bool> AddAsync(Product newProduct, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(newProduct, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

       
        /// Actualiza un producto existente.
        /// Si no existe, devuelve false.
        
        public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Products.AnyAsync(p => p.Id == product.Id, cancellationToken);
            if (!exists) return false;

            product.UpdatedAt = DateTime.UtcNow; // Actualiza la fecha de modificación
            _context.Products.Update(product);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        
        /// Elimina un producto por su ID.
        
        public async Task<bool> DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(new object[] { productId }, cancellationToken);
            if (product == null) return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        
        /// Verifica si existe un producto con el ID indicado.
       
        public async Task<bool> ExistsAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.AnyAsync(p => p.Id == productId, cancellationToken);
        }
    }
}
