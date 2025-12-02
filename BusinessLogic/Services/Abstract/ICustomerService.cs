using BusinessLogic.Dtos;

namespace BusinessLogic.Services.Abstract
{
    public interface ICustomerService : IBaseService<CustomerDto, DataAccess.Entities.Customer>
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync(CustomerFilterDto? filter = null);
        Task<CustomerDto> CreateAsync(CustomerCreateDto createDto);
        Task<CustomerDto> UpdateAsync(int id, CustomerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

