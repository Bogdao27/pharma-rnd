using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public class BatchComponent : MyEntity<BatchComponentId>
    {
        private bool _isDeleted = false;

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

        public static Result<BatchComponent, Error> Create(BatchComponentId id, MassInGrams mass, RawMaterialLotId lotId)
        {
            if (mass == null)
            {
                return Errors.General.ValueIsInvalid("BatchComponent");
            }

            if(lotId == null)
            {
                return Errors.General.ValueIsInvalid("BatchComponent");
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
