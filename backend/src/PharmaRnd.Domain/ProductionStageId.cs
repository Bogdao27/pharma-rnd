namespace PharmaRnd.Domain
{
    public record ProductionStageId
    {
        private ProductionStageId(Guid id)
        {
            Value = id;
        }
        
        public Guid Value { get; }

        public static ProductionStageId NewProductionStageId() => new(Guid.NewGuid());

        public static ProductionStageId Create(Guid id) => new(id); 
    }
}
