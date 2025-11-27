using DataAccess.Context;
using DataAccess.Repositories.BaseRepository;
using DataAccess.Repositories.Concret;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Entities;

namespace DataAccess.DependencyInjection
{
    public static class RepositoryConfiguration
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(context =>
            {
                context.UseSqlServer(configuration.GetConnectionString("storeConnection"));
            });

            services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
        }
    }
}
