using Microsoft.Extensions.DependencyInjection;
using PharmaRnd.Application.Products;
using PharmaRnd.Infrastructure.Repositories;

namespace PharmaRnd.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddScoped<IProductsRepository, ProductsRepository>();

            return services;
        }
    }
}
