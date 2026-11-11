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

        public static ProductId Create(Guid id) => new(id);

        public static implicit operator Guid(ProductId productId)
        {
            if(productId is null)
            {
                throw new ArgumentNullException();
            }

            return productId.Value;
        }
    }
}
