using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaRnd.Domain
{
    public record BatchComponentId
    {
        //приватный конструктор, присваивающий айдишнику это значение
        private BatchComponentId(Guid value)
        {
            Value = value;
        }
        
        //значение айдишника компонента состава
        public Guid Value { get; }

        //метод для создания нового айдишника
        public static BatchComponentId NewBatchComponentId() => new(Guid.NewGuid());

        //для конфига бд
        public static BatchComponentId Create(Guid id) => new(id);
    }
}
