using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PharmaRnd.Application.Products;
using PharmaRnd.Infrastructure.Interceptors;
using PharmaRnd.Infrastructure.Repositories;
using PharmaRnd.Infrastructure.Options;

namespace PharmaRnd.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddSingleton<SoftDeleteInterceptor>();

            services.AddScoped<IProductsRepository, ProductsRepository>();

            services.AddMinio(configuration);

            return services;
        }

        private static IServiceCollection AddMinio(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MyMinioOptions>(configuration.GetSection(MyMinioOptions.MINIO));

            services.AddMinio(options =>
            {
                services.Configure<MyMinioOptions>(configuration.GetSection(MyMinioOptions.MINIO));

                services.AddMinio(options =>
                {
                    var minioOptions = configuration.GetSection(MyMinioOptions.MINIO).Get<MyMinioOptions>()
                                       ?? throw new ApplicationException("Missing minio configuration");

                    options.WithEndpoint(minioOptions.Endpoint);
                    options.WithCredentials("minioadmin", "minioadmin");
                    options.WithSSL(minioOptions.WithSSL);
                });
            });

            return services;
        }
    }
}
