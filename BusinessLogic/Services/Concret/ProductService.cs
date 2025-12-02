using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Services.Abstract;
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;
using System.Linq;

namespace BusinessLogic.Services.Concret
{
    public class ProductService : BaseService<ProductDto, Product>, IProductService
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IBaseRepository<Product> baseRepository, 
            IBaseRepository<Category> categoryRepository,
            IMapper mapper) 
            : base(baseRepository, mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductFilterDto? filter = null)
        {
            var allProducts = await GetAll();
            
            if (filter == null)
            {
                return allProducts;
            }

            var filtered = allProducts.AsQueryable();

            if (filter.CategoryId.HasValue)
            {
                filtered = filtered.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                filtered = filtered.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                filtered = filtered.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            if (filter.MinStock.HasValue)
            {
                filtered = filtered.Where(p => p.Stock >= filter.MinStock.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                filtered = filtered.Where(p => p.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
            }

            return filtered;
        }

        public async Task<ProductDto> CreateAsync(ProductCreateDto createDto)
        {
            // Validar que la categoría existe
            var category = await _categoryRepository.Get(createDto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException($"Categoría con ID {createDto.CategoryId} no encontrada");
            }

            var productDto = _mapper.Map<ProductDto>(createDto);
            var id = await Insert(productDto);
            return await Get(id) ?? productDto;
        }

        public async Task<ProductDto> UpdateAsync(int id, ProductDto dto)
        {
            if (id != dto.Id)
            {
                throw new ArgumentException("El ID de la ruta no coincide con el ID del cuerpo");
            }

            var existing = await Get(id);
            if (existing == null)
            {
                throw new ArgumentException($"Producto con ID {id} no encontrado");
            }
            
            if (existing.CategoryId != dto.CategoryId)
            {
                var category = await _categoryRepository.Get(dto.CategoryId);
                if (category == null)
                {
                    throw new ArgumentException($"Categoría con ID {dto.CategoryId} no encontrada");
                }
            }

            Update(dto);
            return await Get(id) ?? dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await Get(id);
            if (product == null)
            {
                return false;
            }

            Delete(product);
            return true;
        }
    }
}

