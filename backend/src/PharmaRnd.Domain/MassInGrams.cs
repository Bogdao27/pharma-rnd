using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public record MassInGrams
    {
        private MassInGrams(float value)
        {
            Value = value;
        }

        public float Value { get; }

        public static Result<MassInGrams, Error> Create(float value)
        {
            if (!float.IsFinite(value))
            {
                return Errors.General.ValueIsInvalid("MassInGrams");
            }
            
            if (value <= 0)
            {
                return Errors.General.ValueIsInvalid("MassInGrams");
            }
            
            return new MassInGrams(value);
        }
    }
}
