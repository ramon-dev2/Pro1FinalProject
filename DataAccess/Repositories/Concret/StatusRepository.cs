using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.BaseRepository;
using DataAccess.Entities;

namespace DataAccess.Repositories.Concret
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        public StatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

