using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public record DosageForm
    {
        public const int MAX_DOSAGE_FORM_LENGTH = 30;

        private DosageForm(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<DosageForm, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > MAX_DOSAGE_FORM_LENGTH)
            {
                return Errors.General.ValueIsInvalid("DosageForm");
            }

            return new DosageForm(value);
        }
    }
}
