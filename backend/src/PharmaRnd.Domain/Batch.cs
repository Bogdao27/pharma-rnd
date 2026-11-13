using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    //корень агрегата
    public class Batch : MyEntity<BatchId>, ISoftDeletable
    {
        private bool _isDeleted = false;

        private readonly List<BatchComponent> _components = [];

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
        
        public IReadOnlyList<BatchComponent> Components => _components
            .AsReadOnly();

        public Result AddComponent(BatchComponent? batchComponent)
        {
            if(batchComponent == null)
            {
                return Result.Failure("Такого компонента нет");
            }

            _components.Add(batchComponent);

            return Result.Success();
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
