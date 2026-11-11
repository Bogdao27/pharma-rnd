using Microsoft.Extensions.DependencyInjection;
using PharmaRnd.Application.Products.CreateProducts;
using FluentValidation;

namespace PharmaRnd.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateProductHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
