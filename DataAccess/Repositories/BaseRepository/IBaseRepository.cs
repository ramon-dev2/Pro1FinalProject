using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.BaseRepository
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(int id);
        Task<int> Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
