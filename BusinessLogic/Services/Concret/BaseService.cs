using AutoMapper;
using BusinessLogic.Services.Abstract;
using DataAccess.Repositories.BaseRepository;

namespace BusinessLogic.Services.Concret
{
    public class BaseService<TDto, M> : IBaseService<TDto, M> where TDto : class, new()
    {
        private readonly IBaseRepository<M> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<M> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual void Delete(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<M>(dto);
                _baseRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public virtual async Task<TDto?> Get(int id)
        {
            try
            {
                var entity = await _baseRepository.Get(id);
                if (entity == null) return null;
                return _mapper.Map<TDto>(entity);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public virtual async Task<IEnumerable<TDto>> GetAll()
        {
            try
            {
                var entities = await _baseRepository.GetAll();
                return _mapper.Map<IEnumerable<TDto>>(entities);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public virtual async Task<int> Insert(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<M>(dto);
                // Asegurar que el Id sea 0 para nuevas entidades
                var idProperty = entity.GetType().GetProperty("Id");
                if (idProperty != null && idProperty.CanWrite)
                {
                    idProperty.SetValue(entity, 0);
                }
                return await _baseRepository.Insert(entity);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public virtual void Update(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<M>(dto);
                _baseRepository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
