using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

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
