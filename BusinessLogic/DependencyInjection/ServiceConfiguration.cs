using BusinessLogic.Services.Abstract;
using BusinessLogic.Services.Concret;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.DependencyInjection
{
    public static class ServiceConfiguration
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IInventoryMovementService, InventoryMovementService>();
        }
    }
}

