using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    //корень агрегата
    public class Batch : MyEntity<BatchId>
    {
        private readonly List<BatchComponent> _components = [];

        //для ef core
        private Batch(BatchId id) : base(id)
        {
        }

        private Batch
            (
            BatchId id,
            ProductId prodId,
            BatchNumber number,
            DateOnly manufacturedOn
            ) : base(id)
        {
            ProdId = prodId;
            Number = number;
            ManufacturedOn = manufacturedOn;
        }

        public ProductId ProdId { get; } = default!;

        public BatchNumber Number { get; } = default!;

        public DateOnly ManufacturedOn { get; } = default!;
        
        public IReadOnlyList<BatchComponent> Components => _components
            .AsReadOnly();

        public static Result<Batch> Create(
            BatchId id,
            ProductId prodId, 
            BatchNumber number, 
            DateOnly manufacturedOn
            )
        {
            if(id == null)
            {
                return Result.Failure<Batch>("Не указан идентификатор серии");
            }

            if (prodId == null)
            {
                return Result.Failure<Batch>("Не указан идентификатор препарата");
            }

            if (number == null || !number.MatchesManufacturedOn(manufacturedOn))
            {
                return Result.Failure<Batch>("Задайте корректный номер серии");
            }

            return new Batch(id, prodId, number, manufacturedOn);
        }

        public Result AddComponent(BatchComponent? batchComponent)
        {
            if(batchComponent == null)
            {
                return Result.Failure("Такого компонента нет");
            }

            _components.Add(batchComponent);

            return Result.Success();
        }

        public Result RemoveComponent(BatchComponentId id)
        {
            var componentForDelete = Components.FirstOrDefault(x => x.Id == id);
            if(componentForDelete == null)
            {
                return Result.Failure("Такого компонента не существует");
            }

            _components.Remove(componentForDelete);

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
    }
}
