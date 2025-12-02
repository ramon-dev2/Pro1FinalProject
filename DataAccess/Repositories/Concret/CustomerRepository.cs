using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.BaseRepository;
using DataAccess.Entities;

namespace DataAccess.Repositories.Concret
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}

