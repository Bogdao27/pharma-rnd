using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public class ProductionStage : MyEntity<ProductionStageId>, ISoftDeletable
    {
        private bool _isDeleted = false;

        private ProductionStage(ProductionStageId id) : base(id)
        {
        }

        public ProductionStage(
            ProductionStageId id, 
            TimeOnly executedAt, 
            StageDescription stageDescription) : base(id)
        {
            ExecutedAt = executedAt;
            StageDescription = stageDescription;
        }

        public TimeOnly ExecutedAt { get; private set; }
        public StageDescription StageDescription { get; private set; } = default!;

        public void Delete()
        {
            if (_isDeleted == false)
            {
                _isDeleted = true;
            }
        }

        public void Restore()
        {
            if (_isDeleted)
            {
                _isDeleted = false;
            }
        }
    }
}
