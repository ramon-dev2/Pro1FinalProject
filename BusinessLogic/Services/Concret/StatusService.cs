using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Services.Abstract;
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;
using System.Linq;

namespace BusinessLogic.Services.Concret
{
    public class StatusService : BaseService<StatusDto, Status>, IStatusService
    {
        private readonly IBaseRepository<Status> _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IBaseRepository<Status> baseRepository, IMapper mapper) 
            : base(baseRepository, mapper)
        {
            _statusRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatusDto>> GetAllAsync(StatusFilterDto? filter = null)
        {
            var allStatuses = await GetAll();
            
            if (filter == null)
            {
                return allStatuses;
            }

            var filtered = allStatuses.AsQueryable();

            if (filter.IsActive.HasValue)
            {
                filtered = filtered.Where(s => s.IsActive == filter.IsActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                filtered = filtered.Where(s => s.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
            }

            return filtered;
        }

        public async Task<StatusDto> CreateAsync(StatusCreateDto createDto)
        {
            var existingStatus = await _statusRepository.Find(s => s.Name == createDto.Name);
            if (existingStatus != null)
            {
                throw new ArgumentException($"Ya existe un estado con el nombre '{createDto.Name}'");
            }

            var statusDto = _mapper.Map<StatusDto>(createDto);
            var id = await Insert(statusDto);
            return await Get(id) ?? statusDto;
        }

        public async Task<StatusDto> UpdateAsync(int id, StatusDto dto)
        {
            if (id != dto.Id)
            {
                throw new ArgumentException("El ID de la ruta no coincide con el ID del cuerpo");
            }

            var existing = await Get(id);
            if (existing == null)
            {
                throw new ArgumentException($"Estado con ID {id} no encontrado");
            }

       
            if (existing.Name != dto.Name)
            {
                var statusWithName = await _statusRepository.Find(s => s.Name == dto.Name);
                if (statusWithName != null && statusWithName.Id != id)
                {
                    throw new ArgumentException($"Ya existe otro estado con el nombre '{dto.Name}'");
                }
            }

            Update(dto);
            return await Get(id) ?? dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var status = await Get(id);
            if (status == null)
            {
                return false;
            }

            Delete(status);
            return true;
        }

        public async Task<IEnumerable<StatusDto>> GetActiveAsync()
        {
            var allStatuses = await GetAll();
            return allStatuses.Where(s => s.IsActive);
        }
    }
}

