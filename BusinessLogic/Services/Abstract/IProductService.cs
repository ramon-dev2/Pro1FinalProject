using BusinessLogic.Dtos;

namespace BusinessLogic.Services.Abstract
{
    public interface IProductService : IBaseService<ProductDto, DataAccess.Entities.Product>
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(ProductFilterDto? filter = null);
        Task<ProductDto> CreateAsync(ProductCreateDto createDto);
        Task<ProductDto> UpdateAsync(int id, ProductDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

