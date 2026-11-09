using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaRnd.Domain
{
    //указывает на партию сырья, которая использовалась при производстве
    public record RawMaterialLotId
    {
        private RawMaterialLotId(Guid value)
        {
            Value = value;
        }
        
        public Guid Value { get; }

        public static RawMaterialLotId NewRawMaterialLotId() => new(Guid.NewGuid());
    }
}
