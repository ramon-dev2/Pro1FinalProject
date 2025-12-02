using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Services.Abstract;
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BusinessLogic.Services.Concret
{
    public class InventoryMovementService : BaseService<InventoryMovementDto, InventoryMovement>, IInventoryMovementService
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InventoryMovementService(
            IBaseRepository<InventoryMovement> baseRepository,
            IBaseRepository<Product> productRepository,
            IProductService productService,
            ApplicationDbContext context,
            IMapper mapper) 
            : base(baseRepository, mapper)
        {
            _productRepository = productRepository;
            _productService = productService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryMovementDto>> GetAllAsync(InventoryMovementFilterDto? filter = null)
        {
            var allMovements = await GetAll();
            
            if (filter == null)
            {
                return allMovements;
            }

            var filtered = allMovements.AsQueryable();

            if (filter.ProductId.HasValue)
            {
                filtered = filtered.Where(m => m.ProductId == filter.ProductId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.MovementType))
            {
                filtered = filtered.Where(m => m.MovementType == filter.MovementType);
            }

            if (filter.DateFrom.HasValue)
            {
                filtered = filtered.Where(m => m.MovementDate >= filter.DateFrom.Value);
            }

            if (filter.DateTo.HasValue)
            {
                filtered = filtered.Where(m => m.MovementDate <= filter.DateTo.Value);
            }

            return filtered;
        }
        

        public async Task<int> GetCurrentStockAsync(int productId)
        {
            var product = await _productRepository.Get(productId);
            if (product == null)
            {
                throw new ArgumentException($"Producto con ID {productId} no encontrado");
            }
            return product.Stock;
        }

        public async Task<IEnumerable<ProductStockDto>> GetStockByProductsAsync()
        {
            var products = await _productService.GetAllAsync();
            var stockByProducts = products.Select(p => new ProductStockDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                CategoryName = p.CategoryName,
                CurrentStock = p.Stock,
                Price = p.Price
            }).OrderBy(p => p.ProductName);

            return stockByProducts;
        }

        private async Task UpdateProductStockAsync(int productId, string movementType, int quantity)
        {
            var product = await _context.Set<Product>().FindAsync(productId);
            if (product == null) return;

            switch (movementType.ToUpper())
            {
                case "ENTRY":
                case "RETURN":
                    product.Stock += quantity;
                    break;
                case "EXIT":
                case "SALE":
                    product.Stock -= quantity;
                    if (product.Stock < 0) product.Stock = 0; 
                    break;
                case "ADJUSTMENT":
                    product.Stock += quantity;
                    if (product.Stock < 0) product.Stock = 0;
                    break;
            }

            await _context.SaveChangesAsync();
        }
    }
}

