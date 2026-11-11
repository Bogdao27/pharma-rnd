using CSharpFunctionalExtensions;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Products
{
    public interface IProductsRepository
    {
        Task<Guid> Add(Product product, CancellationToken cancellationToken = default);
        Task<Result<Product, Error>> GetById(ProductId productId);
        Task<Result<Product, Error>> GetByProductName(ProductName productName);
    }
}
