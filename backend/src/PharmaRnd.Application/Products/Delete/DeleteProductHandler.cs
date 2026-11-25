using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PharmaRnd.Application.Database;
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
        public DeleteProductHandler(
            IProductsRepository productsRepository,
            ILogger<DeleteProductHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _productsRepository = productsRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        private readonly IProductsRepository _productsRepository;
        private readonly ILogger<DeleteProductHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

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

            productResult.Value.Delete();

            await _unitOfWork.SaveChanges();

            _logger.LogInformation(
                "Updated deleted with id {productId}", 
                request.ProductId);

            return productResult.Value.Id.Value;
        }
    }
}
