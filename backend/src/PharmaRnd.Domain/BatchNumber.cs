using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public record BatchNumber
    {
        public const int BATCH_NUMBER_LENGTH = 6;

        private BatchNumber(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<BatchNumber, Error> Create(string value)
        {
            //если передано ничего или длина строки не равна 6 вернуть ошибку
            if(value == null || value.Length != BATCH_NUMBER_LENGTH)
            {
                return Errors.General.ValueIsInvalid("BatchNumber");
            }

            //Если не все символы цифры, то надо вернуть ошибку 
            if(!value.All(char.IsAsciiDigit))
            {
                return Errors.General.ValueIsInvalid("BatchNumber");
            }

            //проверка правильности написания месяца от 01 до 12
            int month = int.Parse(value.Substring(2, 2));
            if(month < 1 || month > 12)
            {
                return Errors.General.ValueIsInvalid("BatchNumber");
            }

            int sequenceNumber = int.Parse(value[..2]);
            if(sequenceNumber == 0)
            {
                return Errors.General.ValueIsInvalid("BatchNumber");
            }

            var BatchNumber = new BatchNumber(value);
            
            return BatchNumber;
        }

        //проверка на то, что месяц и год производства серии совпадают с введенной датой
        //производства
        public bool MatchesManufacturedOn(DateOnly manufacturedOn)
        {
            int month = int.Parse(Value.Substring(2, 2));
            int year = int.Parse(Value.Substring(4, 2));

            if(month == manufacturedOn.Month && year == manufacturedOn.Year % 100)
            {
                return true;
            }

            return false;
        }
    }
}
