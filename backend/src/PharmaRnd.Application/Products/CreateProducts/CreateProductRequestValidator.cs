using FluentValidation;
using PharmaRnd.Application.Validators;
using PharmaRnd.Domain;

namespace PharmaRnd.Application.Products.CreateProducts
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(c => c.ProductName).MustBeValueObject(ProductName.Create);
            RuleFor(c => c.Description).MustBeValueObject(Description.Create);
            RuleFor(c => c.DosageForm).MustBeValueObject(DosageForm.Create);
        }
    }
}
