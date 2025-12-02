using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.BaseRepository;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concret
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Set<Order>()
                .Include(o => o.Customer)
                .Include(o => o.Status)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => !o.IsDeleted)
                .AsNoTracking()
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public override async Task<Order?> Get(int id)
        {
            var order = await _context.Set<Order>()
                .Include(o => o.Customer)
                .Include(o => o.Status)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
            
            if (order != null && order.IsDeleted)
            {
                return null;
            }
            
            return order;
        }
    }
}

