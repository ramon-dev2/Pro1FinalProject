using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Services.Abstract;
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;
using System.Linq;

namespace BusinessLogic.Services.Concret
{
    public class CategoryService : BaseService<CategoryDto, Category>, ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryService(IBaseRepository<Category> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(CategoryFilterDto? filter = null)
        {
            var allCategories = await GetAll();
            
            if (filter == null)
                return allCategories;
            

            var filtered = allCategories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                filtered = filtered.Where(c => c.Name != null && c.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
            }

            return filtered;
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto createDto)
        {
            var categoryDto = _mapper.Map<CategoryDto>(createDto);
            var id = await Insert(categoryDto);
            return await Get(id) ?? categoryDto;
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryDto dto)
        {
            if (id != dto.Id)
            {
                throw new ArgumentException("El ID de la ruta no coincide con el ID del cuerpo");
            }

            var existing = await Get(id);
            if (existing == null)
            {
                throw new ArgumentException($"Categoría con ID {id} no encontrada");
            }

            Update(dto);
            return await Get(id) ?? dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await Get(id);
            if (category == null)
            {
                return false;
            }

            Delete(category);
            return true;
        }
    }
}

