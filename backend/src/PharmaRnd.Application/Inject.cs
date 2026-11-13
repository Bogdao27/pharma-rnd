using Microsoft.Extensions.DependencyInjection;
using PharmaRnd.Application.Products.CreateProducts;
using FluentValidation;
using PharmaRnd.Application.Products.UpdateMainInfo;
using PharmaRnd.Application.Products.Delete;

namespace PharmaRnd.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateProductHandler>();

            services.AddScoped<UpdateMainInfoHandler>();
            
            services.AddScoped<DeleteProductHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
