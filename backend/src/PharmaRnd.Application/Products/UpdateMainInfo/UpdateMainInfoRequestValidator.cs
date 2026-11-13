using FluentValidation;
using PharmaRnd.Application.Validators;
using PharmaRnd.Domain;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Application.Products.UpdateMainInfo
{
    public class UpdateMainInfoRequestValidator : AbstractValidator<UpdateMainInfoRequest>
    {
        public UpdateMainInfoRequestValidator()
        {
            RuleFor(r => r.ProductId).NotEmpty().WithError(Errors.General.ValueIsRequiered());
        }
    }

    public class UpdateMainInfoDtoValidator : AbstractValidator<UpdateMainInfoDto>
    {
        public UpdateMainInfoDtoValidator()
        {
            RuleFor(r => r.ProductName).MustBeValueObject(ProductName.Create);
            RuleFor(r => r.Description).MustBeValueObject(Description.Create);
            RuleFor(r => r.DosageForm).MustBeValueObject(DosageForm.Create);
        }
    }
}
