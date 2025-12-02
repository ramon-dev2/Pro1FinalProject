using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.BaseRepository;
using DataAccess.Repositories.Concret;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Entities;

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

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBaseRepository<Product>, ProductRepository>();
            
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBaseRepository<Customer>, CustomerRepository>();
            
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IBaseRepository<Status>, StatusRepository>();
            
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBaseRepository<Order>, OrderRepository>();
            
            services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
            services.AddScoped<IBaseRepository<InventoryMovement>, InventoryMovementRepository>();
        }
    }
}
