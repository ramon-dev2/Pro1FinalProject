using BusinessLogic.Dtos;

namespace BusinessLogic.Services.Abstract
{
    public interface IStatusService : IBaseService<StatusDto, DataAccess.Entities.Status>
    {
        Task<IEnumerable<StatusDto>> GetAllAsync(StatusFilterDto? filter = null);
        Task<StatusDto> CreateAsync(StatusCreateDto createDto);
        Task<StatusDto> UpdateAsync(int id, StatusDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<StatusDto>> GetActiveAsync();
    }
}

