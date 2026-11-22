using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public record StageDescription
    {
        public const int MAX_LENGTH = 1000;

        private StageDescription(string value)
        {
            Value = value;
        }
        
        public string Value { get; }

        public static Result<StageDescription, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_LENGTH)
            {
                return Errors.General.ValueIsInvalid("StageDescription");
            }

            return new StageDescription(value);
        }
    }
}
