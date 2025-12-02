using BusinessLogic.Dtos;
using BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryMovementController : ControllerBase
    {
        private readonly IInventoryMovementService _inventoryMovementService;

        public InventoryMovementController(IInventoryMovementService inventoryMovementService)
        {
            _inventoryMovementService = inventoryMovementService;
        }

        [HttpGet("stock")]
        public async Task<ActionResult<IEnumerable<ProductStockDto>>> GetStockByProducts()
        {
            try
            {
                var stockByProducts = await _inventoryMovementService.GetStockByProductsAsync();
                return Ok(stockByProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener stock por productos: {ex.Message}");
            }
        }
    }
}

