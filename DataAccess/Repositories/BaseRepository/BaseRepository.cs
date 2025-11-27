using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual async Task<T?> Get(int id) => await _context.Set<T>().FindAsync(id);

        public virtual async Task<T?> Find(Expression<Func<T, bool>> predicate) => await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate); 

        public virtual async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().AsNoTracking().ToListAsync();

        public virtual async Task<int> Insert(T entity)
        {
            _context.Add(entity);
            return Convert.ToInt32(_context.SaveChanges());
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
