using CSharpFunctionalExtensions;

namespace PharmaRnd.Domain
{
    public class MassInGrams : ValueObject
    {
        //Приватный конструктор, присваивает значение
        private MassInGrams(float value)
        {
            Value = value;
        }

        //Свойство, отражающее значение массы в граммах
        //можем получить извне
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

            var massInGrams = new MassInGrams(value);
            return massInGrams;
        }

        //Метод для сравнения двух объектов. Равенство двух объектов
        //определяется значением массы
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
