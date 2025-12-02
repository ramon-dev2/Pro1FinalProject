using BusinessLogic.Dtos;

namespace BusinessLogic.Services.Abstract
{
    public interface IInventoryMovementService : IBaseService<InventoryMovementDto, DataAccess.Entities.InventoryMovement>
    {
        Task<IEnumerable<InventoryMovementDto>> GetAllAsync(InventoryMovementFilterDto? filter = null);
        Task<int> GetCurrentStockAsync(int productId);
        Task<IEnumerable<ProductStockDto>> GetStockByProductsAsync();
    }
}

