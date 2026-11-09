using CSharpFunctionalExtensions;

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

        public static Result<BatchNumber> Create(string value)
        {
            //если передано ничего или длина строки не равна 6 вернуть ошибку
            if(value == null || value.Length != BATCH_NUMBER_LENGTH)
            {
                return Result.Failure<BatchNumber>("Укажите корректный номер серии");
            }

            //Если не все символы цифры, то надо вернуть ошибку 
            if(!value.All(char.IsAsciiDigit))
            {
                return Result.Failure<BatchNumber>("Укажите корректный номер серии");
            }

            //проверка правильности написания месяца от 01 до 12
            int month = int.Parse(value.Substring(2, 2));
            if(month < 1 || month > 12)
            {
                return Result.Failure<BatchNumber>("Недопустимый номер месяца");
            }

            int sequenceNumber = int.Parse(value[..2]);
            if(sequenceNumber == 0)
            {
                return Result.Failure<BatchNumber>("Порядковый номер не может быть равен 0");
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
