using CSharpFunctionalExtensions;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Products
{
    public interface IProductsRepository
    {
        Task<Guid> Add(Product product, CancellationToken cancellationToken = default);
        Task<Result<Product, Error>> GetById(
            ProductId productId, 
            CancellationToken cancellationToken = default);
        Task<Result<Product, Error>> GetByProductName(
            ProductName productName, 
            CancellationToken cancellationToken = default);
        Guid Save(Product product, CancellationToken cancellationToken = default);
        Guid Delete(Product product, CancellationToken cancellationToken = default);
    }
}
