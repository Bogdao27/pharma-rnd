using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public record ProductName
    {
        public const int MAX_PRODUCT_NAME_LENGTH = 50;

        private ProductName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<ProductName, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_PRODUCT_NAME_LENGTH)
            {
                return Errors.General.ValueIsInvalid("ProductName");
            }

            return new ProductName(value);
        }
    }
}
