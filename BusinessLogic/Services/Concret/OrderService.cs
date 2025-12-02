using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Services.Abstract;
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;
using System.Linq;

namespace BusinessLogic.Services.Concret
{
    public class OrderService : BaseService<OrderDto, Order>, IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<Customer> _customerRepository;
        private readonly IBaseRepository<Status> _statusRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IBaseRepository<Order> baseRepository, 
            IBaseRepository<Product> productRepository,
            IBaseRepository<Customer> customerRepository,
            IBaseRepository<Status> statusRepository,
            IMapper mapper) 
            : base(baseRepository, mapper)
        {
            _orderRepository = baseRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync(OrderFilterDto? filter = null)
        {
            var allOrders = await GetAll();
            
            if (filter == null)
            {
                return allOrders;
            }

            var filtered = allOrders.AsQueryable();

            if (filter.CustomerId.HasValue)
            {
                filtered = filtered.Where(o => o.CustomerId == filter.CustomerId.Value);
            }

            if (filter.StatusId.HasValue)
            {
                filtered = filtered.Where(o => o.StatusId == filter.StatusId.Value);
            }

            if (filter.DateFrom.HasValue)
            {
                filtered = filtered.Where(o => o.OrderDate >= filter.DateFrom.Value);
            }

            if (filter.DateTo.HasValue)
            {
                filtered = filtered.Where(o => o.OrderDate <= filter.DateTo.Value);
            }

            if (filter.MinTotal.HasValue)
            {
                filtered = filtered.Where(o => o.TotalAmount >= filter.MinTotal.Value);
            }

            if (filter.MaxTotal.HasValue)
            {
                filtered = filtered.Where(o => o.TotalAmount <= filter.MaxTotal.Value);
            }

            return filtered;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var customer = await _customerRepository.Get(orderCreateDto.CustomerId);
            if (customer == null)
            {
                throw new ArgumentException($"Cliente con ID {orderCreateDto.CustomerId} no encontrado");
            }
            
            var status = await _statusRepository.Get(orderCreateDto.StatusId);
            if (status == null)
            {
                throw new ArgumentException($"Estado con ID {orderCreateDto.StatusId} no encontrado");
            }

            if (!status.IsActive)
            {
                throw new ArgumentException($"El estado '{status.Name}' no está activo y no puede ser asignado");
            }
            
            var orderItems = new List<OrderItemDto>();
            decimal totalAmount = 0;

            foreach (var item in orderCreateDto.OrderItems)
            {
                
                var product = await _productRepository.Get(item.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"Producto con ID {item.ProductId} no encontrado");
                }
                
                if (product.Stock < item.Quantity)
                {
                    throw new ArgumentException($"Stock insuficiente para el producto '{product.Name}'. Stock disponible: {product.Stock}, solicitado: {item.Quantity}");
                }
                
                var unitPrice = product.Price;
                var subtotal = unitPrice * item.Quantity;
                totalAmount += subtotal;

                orderItems.Add(new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    Subtotal = subtotal
                });
            }
            
            
            var orderDto = new OrderDto
            {
                CustomerId = orderCreateDto.CustomerId,
                StatusId = orderCreateDto.StatusId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                OrderItems = orderItems
            };
            
            var orderId = await Insert(orderDto);
            orderDto.Id = orderId;
            
            return await Get(orderId) ?? orderDto;
        }

        public async Task<OrderDto> UpdateOrderStatusAsync(int orderId, int statusId)
        {
            var order = await Get(orderId);
            if (order == null)
            {
                throw new ArgumentException($"Orden con ID {orderId} no encontrada");
            }
            
            
            var status = await _statusRepository.Get(statusId);
            if (status == null)
            {
                throw new ArgumentException($"Estado con ID {statusId} no encontrado");
            }

            if (!status.IsActive)
            {
                throw new ArgumentException($"El estado '{status.Name}' no está activo y no puede ser asignado");
            }
            
            order.StatusId = statusId;
            Update(order);
            
            return await Get(orderId) ?? order;
        }

        public override void Update(OrderDto dto)
        {
            if (dto.OrderItems != null && dto.OrderItems.Any())
            {
                dto.TotalAmount = dto.OrderItems.Sum(item => item.Subtotal);
            }

            base.Update(dto);
        }
    }
}

