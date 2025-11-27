using Store.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface ICategoryRepository
    {
       IEnumerable<Category> GetAll();
       Category Post(Category category);
    }
}
