using CSharpFunctionalExtensions;
using FluentValidation;
using PharmaRnd.Application.Validators;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Products.UpdateMainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IProductsRepository _productsRepository;

        public UpdateMainInfoHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
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

            var result = await _productsRepository.Save(productResult.Value, cancellationToken);

            return result;
        }
    }
}
