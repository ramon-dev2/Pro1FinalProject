using DataAccess.Context;
using DataAccess.Repositories.BaseRepository;
using Store.Entities;

namespace DataAccess.Repositories.Concret
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
