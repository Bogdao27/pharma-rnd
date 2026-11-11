using CSharpFunctionalExtensions;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Products.CreateProducts
{
    public class CreateProductHandler
    {
        public CreateProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        
        private readonly IProductsRepository _productsRepository;

        public async Task<Result<Guid, Error>> HandlerAsync(
            CreateProductRequest request,
            CancellationToken cancellationToken = default)
        {
            var productName = ProductName.Create(request.ProductName).Value;
            var description = Description.Create(request.Description).Value;
            var dosageForm = DosageForm.Create(request.DosageForm).Value;

            var product = await _productsRepository.GetByProductName(productName);
            if (product.IsSuccess)
            {
                return Errors.Product.AlreadyExist();
            }

            var productId = ProductId.NewProductId();

            var productToCreate = new Product(productId, productName, description, dosageForm);

            await _productsRepository.Add(productToCreate, cancellationToken);

            return (Guid)productToCreate.Id;
        }
    }
}
