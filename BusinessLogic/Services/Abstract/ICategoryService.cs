using BusinessLogic.Dtos;

namespace BusinessLogic.Services.Abstract
{
    public interface ICategoryService : IBaseService<CategoryDto, DataAccess.Entities.Category>
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync(CategoryFilterDto? filter = null);
        Task<CategoryDto> CreateAsync(CategoryCreateDto createDto);
        Task<CategoryDto> UpdateAsync(int id, CategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

