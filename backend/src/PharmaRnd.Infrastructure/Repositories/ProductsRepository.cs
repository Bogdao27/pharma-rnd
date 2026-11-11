using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PharmaRnd.Application.Products;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        public ProductsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly ApplicationDbContext _dbContext;

        public async Task<Guid> Add(Product product, CancellationToken cancellationToken = default)
        {
            await _dbContext.Products.AddAsync(product, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

        public async Task<Result<Product, Error>> GetById(ProductId productId)
        {
            var product = await _dbContext.Products
                .Include(p => p.Batches)
                .ThenInclude(b => b.Components)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null)
            {
                return Errors.General.NotFound(productId);
            }

            return product;
        }

        public async Task<Result<Product, Error>> GetByProductName(ProductName productName)
        {
            var product = await _dbContext.Products
                .Include(p => p.Batches)
                .ThenInclude(b => b.Components)
                .FirstOrDefaultAsync(p => p.ProductName == productName);

            if (product is null)
            {
                return Errors.General.NotFound();
            }

            return product;
        }
    }
}
