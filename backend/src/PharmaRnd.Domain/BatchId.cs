namespace PharmaRnd.Domain
{
    //идентификатор самой серии в системе
    public record BatchId
    {
        private BatchId(Guid value)
        {
            Value = value;
        }
        
        public Guid Value { get; }

        public static BatchId NewBatchId() => new(Guid.NewGuid());

        //для конфига бд
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
