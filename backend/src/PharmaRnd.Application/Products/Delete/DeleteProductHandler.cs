using CSharpFunctionalExtensions;
using PharmaRnd.Application.Products.UpdateMainInfo;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaRnd.Application.Products.Delete
{
    public class DeleteProductHandler
    {
        public DeleteProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        private readonly IProductsRepository _productsRepository;

        public async Task<Result<Guid, Error>> HandlerAsync(
            DeleteProductRequest request,
            CancellationToken cancellationToken = default)
        {
            var productResult = await _productsRepository
                .GetById(request.ProductId, cancellationToken);
            if (productResult.IsFailure)
            {
                return productResult.Error;
            }

            var result = await _productsRepository.Delete(
                productResult.Value, 
                cancellationToken);

            return result;
        }
    }
}
