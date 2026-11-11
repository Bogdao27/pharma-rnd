using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public record Description
    {
        public const int MAX_DESCRIPTION_LENGTH = 300;

        private Description(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<Description, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_DESCRIPTION_LENGTH)
            {
                return Errors.General.ValueIsInvalid("Description");
            }

            return new Description(value);
        }
    }
}
