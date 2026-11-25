using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PharmaRnd.Application.Database;
using PharmaRnd.Application.Validators;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Products.UpdateMainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ILogger<UpdateMainInfoHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMainInfoHandler(
            IProductsRepository productsRepository,
            ILogger<UpdateMainInfoHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _productsRepository = productsRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, Error>> HandlerAsync(
            UpdateMainInfoRequest request,
            CancellationToken cancellationToken = default)
        {
            var productResult = await _productsRepository
                .GetById(request.ProductId, cancellationToken);
            if (productResult.IsFailure)
            {
                return productResult.Error;
            }

            var productName = ProductName.Create(request.Dto.ProductName).Value;
            var description = Description.Create(request.Dto.Description).Value;
            var dosageForm = DosageForm.Create(request.Dto.DosageForm).Value;

            productResult.Value.UpdateMainInfo(productName, description, dosageForm);

            await _unitOfWork.SaveChanges(cancellationToken);

            _logger.LogInformation(
                "Updated product {productName}, {description}, {dosageForm} with id {productId}",
                productName,
                description,
                dosageForm,
                request.ProductId);

            return productResult.Value.Id.Value;
        }
    }
}
