using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual void Delete(T entity)
        {
            var isDeletedProperty = entity.GetType().GetProperty("IsDeleted");
            var deletedAtProperty = entity.GetType().GetProperty("DeletedAt");
            
            if (isDeletedProperty != null && isDeletedProperty.CanWrite)
            {
                isDeletedProperty.SetValue(entity, true);
            }
            
            if (deletedAtProperty != null && deletedAtProperty.CanWrite)
            {
                deletedAtProperty.SetValue(entity, DateTime.UtcNow);
            }
            
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public virtual async Task<T?> Get(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return null;
            
            // Verificar si está eliminado lógicamente
            var isDeletedProperty = entity.GetType().GetProperty("IsDeleted");
            if (isDeletedProperty != null)
            {
                var isDeleted = isDeletedProperty.GetValue(entity) as bool?;
                if (isDeleted == true) return null;
            }
            
            return entity;
        }

        public virtual async Task<T?> Find(Expression<Func<T, bool>> predicate)
        {
            var query = _context.Set<T>().AsNoTracking();
            var isDeletedProperty = typeof(T).GetProperty("IsDeleted");
            if (isDeletedProperty != null)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, isDeletedProperty);
                var falseConstant = Expression.Constant(false);
                var notDeletedCondition = Expression.Equal(property, falseConstant);
                var notDeletedLambda = Expression.Lambda<Func<T, bool>>(notDeletedCondition, parameter);
                query = query.Where(notDeletedLambda);
            }
            
            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var query = _context.Set<T>().AsNoTracking();
            
            var isDeletedProperty = typeof(T).GetProperty("IsDeleted");
            if (isDeletedProperty != null)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, isDeletedProperty);
                var falseConstant = Expression.Constant(false);
                var condition = Expression.Equal(property, falseConstant);
                var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
                query = query.Where(lambda);
            }
            
            return await query.ToListAsync();
        }

        public virtual async Task<int> Insert(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.GetType().GetProperty("Id")?.GetValue(entity) as int? ?? 0;
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
