using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Services.Abstract;
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;
using System.Linq;

namespace BusinessLogic.Services.Concret
{
    public class CustomerService : BaseService<CustomerDto, Customer>, ICustomerService
    {
        private readonly IBaseRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IBaseRepository<Customer> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {
            _customerRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync(CustomerFilterDto? filter = null)
        {
            var allCustomers = await GetAll();
            
            if (filter == null)
            {
                return allCustomers;
            }

            var filtered = allCustomers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                filtered = filtered.Where(c => c.Email.Contains(filter.Email, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                filtered = filtered.Where(c => c.FirstName.Contains(filter.FirstName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                filtered = filtered.Where(c => c.LastName.Contains(filter.LastName, StringComparison.OrdinalIgnoreCase));
            }

            return filtered;
        }

        public async Task<CustomerDto> CreateAsync(CustomerCreateDto createDto)
        {
            // Validar que el email no exista
            var existingCustomer = await _customerRepository.Find(c => c.Email == createDto.Email);
            if (existingCustomer != null)
            {
                throw new ArgumentException($"Ya existe un cliente con el email {createDto.Email}");
            }

            var customerDto = _mapper.Map<CustomerDto>(createDto);
            customerDto.CreatedAt = DateTime.UtcNow;
            var id = await Insert(customerDto);
            return await Get(id) ?? customerDto;
        }

        public async Task<CustomerDto> UpdateAsync(int id, CustomerDto dto)
        {
            if (id != dto.Id)
            {
                throw new ArgumentException("El ID de la ruta no coincide con el ID del cuerpo");
            }

            var existing = await Get(id);
            if (existing == null)
            {
                throw new ArgumentException($"Cliente con ID {id} no encontrado");
            }
            
            if (existing.Email != dto.Email)
            {
                var customerWithEmail = await _customerRepository.Find(c => c.Email == dto.Email);
                if (customerWithEmail != null && customerWithEmail.Id != id)
                {
                    throw new ArgumentException($"Ya existe otro cliente con el email {dto.Email}");
                }
            }

            Update(dto);
            return await Get(id) ?? dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await Get(id);
            if (customer == null)
            {
                return false;
            }

            Delete(customer);
            return true;
        }
    }
}

