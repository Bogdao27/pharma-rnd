using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PharmaRnd.Application.Products;
using PharmaRnd.Infrastructure.Interceptors;
using PharmaRnd.Infrastructure.Repositories;
using PharmaRnd.Infrastructure.Options;
using PharmaRnd.Application.Providers;
using PharmaRnd.Infrastructure.Providers;

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

            services.AddScoped<IFilesProvider, MinioProvider>();

            return services;
        }

        private static IServiceCollection AddMinio(
            this IServiceCollection services, IConfiguration configuration)
        {
            var minioOptions = configuration.GetSection(MyMinioOptions.MINIO).Get<MyMinioOptions>()
                               ?? throw new ApplicationException("Missing minio configuration");

            services.AddMinio(options =>
            {
                options.WithEndpoint(minioOptions.Endpoint);
                options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
                options.WithSSL(minioOptions.WithSSL);
            });

            return services;
        }
    }
}
