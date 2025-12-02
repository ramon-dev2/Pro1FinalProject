using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.BaseRepository;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concret
{
    public class InventoryMovementRepository : BaseRepository<InventoryMovement>, IInventoryMovementRepository
    {
        public InventoryMovementRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<InventoryMovement>> GetAll()
        {
            // Los movimientos de inventario no se eliminan lógicamente, se mantienen para historial
            return await _context.Set<InventoryMovement>()
                .Include(im => im.Product)
                .AsNoTracking()
                .OrderByDescending(im => im.MovementDate)
                .ToListAsync();
        }

        public override async Task<InventoryMovement?> Get(int id)
        {
            return await _context.Set<InventoryMovement>()
                .Include(im => im.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(im => im.Id == id);
        }
    }
}

