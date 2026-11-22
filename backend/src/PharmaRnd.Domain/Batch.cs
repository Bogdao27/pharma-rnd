using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    //корень агрегата
    public class Batch : MyEntity<BatchId>, ISoftDeletable
    {
        private bool _isDeleted = false;

        private readonly List<BatchComponent> _components = [];
        private readonly List<ProductionStage> _stages = [];

        //для ef core
        private Batch(BatchId id) : base(id)
        {
        }

        public Batch
            (
            BatchId id,
            BatchNumber number,
            DateOnly manufacturedOn
            ) : base(id)
        {
            Number = number;
            ManufacturedOn = manufacturedOn;
        }

        public BatchNumber Number { get; private set; } = default!;

        public DateOnly ManufacturedOn { get; private set; } = default!;
        
        public IReadOnlyList<BatchComponent> Components => _components.AsReadOnly();

        public IReadOnlyList<ProductionStage> Stages => _stages.AsReadOnly();

        public UnitResult<Error> AddComponent(BatchComponent? batchComponent)
        {
            if (_isDeleted)
            {
                return Errors.Batch.IsDeleted();
            }

            if (batchComponent == null)
            {
                return Errors.General.ValueIsInvalid("BatchComponent");
            }

            _components.Add(batchComponent);

            return Result.Success<Error>();
        }

        public UnitResult<Error> AddStage(ProductionStage? productionStage)
        {
            if (_isDeleted)
            {
                return Errors.Batch.IsDeleted();
            }
            
            if(productionStage == null)
            {
                return Errors.General.ValueIsInvalid("ProductionStage");
            }

            _stages.Add(productionStage);

            return Result.Success<Error>();
        }

        public Result ChangeComponentMass(BatchComponentId id, MassInGrams newMass)
        {
            var batchComponent = Components.FirstOrDefault(x => x.Id == id);
            if (batchComponent == null)
            {
                return Result.Failure("Такого компонента не существует");
            }

            return batchComponent.ChangeMass(newMass);
        }

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
