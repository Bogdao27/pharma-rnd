namespace PharmaRnd.Domain
{
    public record BatchId
    {
        private BatchId(Guid value)
        {
            Value = value;
        }
        
        public Guid Value { get; }

        public static BatchId NewBatchId() => new(Guid.NewGuid());

        public static BatchId Create(Guid id) => new(id);

        public static implicit operator Guid(BatchId batchId)
        {
            if (batchId is null)
            {
                throw new ArgumentNullException();
            }

            return batchId.Value;
        }
    }
}
