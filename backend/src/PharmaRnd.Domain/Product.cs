using CSharpFunctionalExtensions;
using PharmaRnd.Domain.Shared;

namespace PharmaRnd.Domain
{
    public class Product : MyEntity<ProductId>, ISoftDeletable
    {
        private bool _isDeleted = false;
        
        private readonly List<Batch> _batches = [];
        
        private Product(ProductId id) : base(id)
        {
        }

        public Product(
            ProductId id,
            ProductName productName,
            Description description,
            DosageForm dosageForm) : base(id)
        {
            ProductName = productName;
            Description = description;
            DosageForm = dosageForm;
        }

        public ProductName ProductName { get; private set; } = default!;
        public Description Description { get; private set; } = default!;
        public DosageForm DosageForm { get; private set; } = default!;
        
        public IReadOnlyList<Batch> Batches => _batches;
        
        public int GetNumberOfBatches() => _batches.Count();

        public void UpdateMainInfo(
            ProductName productName, 
            Description description, 
            DosageForm dosageForm)
        {
            ProductName = productName;
            Description = description;
            DosageForm = dosageForm;
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

        public UnitResult<Error> AddBatch(Batch batch)
        {
            if (_batches.Any(existingBatch => existingBatch.Number == batch.Number))
            {
                return Errors.Batch.AlreadyExist();
            }
            
            _batches.Add(batch);

            return Result.Success<Error>();
        }
    }
}
