using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public class BatchComponent : MyEntity<BatchComponentId>
    {
        //для Ef Core
        private BatchComponent(BatchComponentId id) : base(id)
        {  
        }

        //закрытый конструктор, присваивающий значения этим полям
        private BatchComponent(BatchComponentId id, MassInGrams mass, RawMaterialLotId lotId) : base(id)
        {
            Mass = mass;
            LotId = lotId;
        }

        //свойство масса в виде VO
        public MassInGrams Mass { get; private set; } = default!;

        //свойство айдишник в виде VO
        public RawMaterialLotId LotId { get; private set; } = default!;

        public static Result<BatchComponent> Create(BatchComponentId id, MassInGrams mass, RawMaterialLotId lotId)
        {
            if (mass == null)
            {
                return Result.Failure<BatchComponent>("Укажите массу");
            }

            if(lotId == null)
            {
                return Result.Failure<BatchComponent>("Укажите партию сырья");
            }

            return new BatchComponent(id, mass, lotId);
        } 

        internal Result ChangeMass(MassInGrams newMass)
        {
            if(newMass == null)
            {
                return Result.Failure("Укажите массу");
            }
            
            Mass = newMass;

            return Result.Success();
        }
    }
}
