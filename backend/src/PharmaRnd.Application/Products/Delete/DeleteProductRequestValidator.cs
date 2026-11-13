using FluentValidation;

namespace PharmaRnd.Application.Products.Delete
{
    public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
    {
        public DeleteProductRequestValidator()
        {
            RuleFor(d => d.ProductId).NotEmpty();
        }
    }
}
