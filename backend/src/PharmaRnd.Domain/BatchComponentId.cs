namespace PharmaRnd.Domain
{
    public record BatchComponentId
    {
        private BatchComponentId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static BatchComponentId NewBatchComponentId() => new(Guid.NewGuid());

        public static BatchComponentId Create(Guid id) => new(id);
    }
}
