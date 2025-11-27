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

        public void Delete(TDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<int> Insert(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<M>(dto);
                return await _baseRepository.Insert(entity);
            }catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void Update(TDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
