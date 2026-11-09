using CSharpFunctionalExtensions;

namespace PharmaRnd.Domain
{
    public record MassInGrams
    {
        //Приватный конструктор, присваивает значение
        private MassInGrams(float value)
        {
            Value = value;
        }

        //Свойство, отражающее значение массы в граммах
        //можем получить извне, но не дать новое значение
        public float Value { get; }

        //Метод для создания объекта MassInGrams через приватный конструктор
        //и проверки значения на ноль
        public static Result<MassInGrams> Create(float value)
        {
            if (!float.IsFinite(value))
            {
                return Result.Failure<MassInGrams>("Масса должна быть конечным числом");
            }
            
            if (value <= 0)
            {
                return Result.Failure<MassInGrams>("Масса должна быть больше нуля");
            }
            
            return new MassInGrams(value);
        }
    }
}
