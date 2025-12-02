using BusinessLogic.Dtos;

namespace BusinessLogic.Services.Abstract
{
    public interface IOrderService : IBaseService<OrderDto, DataAccess.Entities.Order>
    {
        Task<IEnumerable<OrderDto>> GetAllAsync(OrderFilterDto? filter = null);
        Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto);
        Task<OrderDto> UpdateOrderStatusAsync(int orderId, int statusId);
    }
}

