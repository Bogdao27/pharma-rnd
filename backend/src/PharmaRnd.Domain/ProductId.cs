using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaRnd.Domain
{
    public record ProductId
    {
        private ProductId(Guid value)
        {
            Value = value;
        }
        
        public Guid Value { get; }

        public static ProductId NewProductId() => new(Guid.NewGuid());
    }
}
