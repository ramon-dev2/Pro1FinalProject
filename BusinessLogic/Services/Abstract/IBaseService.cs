namespace BusinessLogic.Services.Abstract
{
    public interface IBaseService<TDto, M>
    {
        Task<IEnumerable<TDto>> GetAll();
        Task<TDto> Get(int id);
        Task<int> Insert(TDto entity);
        void Update(TDto entity);
        void Delete(TDto entity);
    }
}
