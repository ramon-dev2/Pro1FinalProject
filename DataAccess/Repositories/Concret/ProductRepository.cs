using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.BaseRepository;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concret
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Set<Product>()
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Product?> Get(int id)
        {
            var product = await _context.Set<Product>()
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (product != null && product.IsDeleted)
            {
                return null;
            }
            
            return product;
        }
    }
}

