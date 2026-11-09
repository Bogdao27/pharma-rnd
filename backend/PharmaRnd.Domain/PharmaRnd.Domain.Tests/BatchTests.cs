namespace PharmaRnd.Domain.Tests
{
    public class BatchTests
    {
        [Fact]
        public void Create_WithValidArguments_ReturnsSuccess()
        {
            var batchId = BatchId.NewBatchId();
            var prodId = ProductId.NewProductId();
            var batchNumber = BatchNumber.Create("010226").Value;
            var manufacturedOn = new DateOnly(2026, 2, 15);

            var batch = Batch.Create(batchId, prodId, batchNumber, manufacturedOn);

            Assert.True(batch.IsSuccess);

            Assert.Equal(batchId, batch.Value.Id);
            Assert.Equal(prodId, batch.Value.ProdId);
            Assert.Equal(batchNumber, batch.Value.Number);
            Assert.Equal(manufacturedOn, batch.Value.ManufacturedOn);

            Assert.Empty(batch.Value.Components);
        }

        [Fact]
        public void Create_WithMismatchedManufacturedOn_ReturnsFailure()
        {
            var batchId = BatchId.NewBatchId();
            var prodId = ProductId.NewProductId();
            var batchNumber = BatchNumber.Create("010226").Value;
            var manufacturedOn = new DateOnly(2026, 3, 15);

            var batch = Batch.Create(batchId, prodId, batchNumber, manufacturedOn);

            Assert.True(batch.IsFailure);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(false, false, true)]
        public void Create_WithMissingRequiredArgument_ReturnsFailure(
            bool missingBatchId,
            bool missingProductId,
            bool missingNumber
            )
        {
            var batchId = missingBatchId ? null : BatchId.NewBatchId();
            
            var prodId = missingProductId ? null : ProductId.NewProductId();
            
            var batchNumber = missingNumber ? null : BatchNumber.Create("010226").Value;

            var manufacturedOn = new DateOnly(2026, 2, 15);

            var result = Batch.Create(
                batchId!,
                prodId!,
                batchNumber!,
                manufacturedOn
                );

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void AddComponent_WithValidComponent_AddsToBatch()
        {
            var batchComponentId = BatchComponentId.NewBatchComponentId();
            var massInGrams = MassInGrams.Create(0.45f).Value;
            var rawMaterialLotId = RawMaterialLotId.NewRawMaterialLotId();
            
            var batchComponent = BatchComponent.Create(batchComponentId, massInGrams, rawMaterialLotId).Value;

            var batchId = BatchId.NewBatchId();
            var prodId = ProductId.NewProductId();
            var batchNumber = BatchNumber.Create("010226").Value;
            var manufacturedOn = new DateOnly(2026, 2, 15);

            var batch = Batch.Create(batchId, prodId, batchNumber, manufacturedOn).Value;

            var result = batch.AddComponent(batchComponent);

            Assert.True(result.IsSuccess);

            var addedComponent = Assert.Single(batch.Components);

            Assert.Same(batchComponent, addedComponent);
        }

        [Fact]
        public void AddComponent_WithNullComponent_ReturnsFailure()
        {
            var batchId = BatchId.NewBatchId();
            var prodId = ProductId.NewProductId();
            var batchNumber = BatchNumber.Create("010226").Value;
            var manufacturedOn = new DateOnly(2026, 2, 15);

            var batch = Batch.Create(batchId, prodId, batchNumber, manufacturedOn).Value;

            var result = batch.AddComponent(null);

            Assert.True(result.IsFailure);

            Assert.Empty(batch.Components);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(false, true, false)]
        [InlineData(true, false, false)]
        public void ChangeMassComponent_WithDifferentArgs(
            bool useExitingId,
            bool provideNewMass,
            bool expectedSuccess //ожидаемый результат операции
            )
        {
            var batchId = BatchId.NewBatchId();
            var prodId = ProductId.NewProductId();
            var batchNumber = BatchNumber.Create("010226").Value;
            var manufacturedOn = new DateOnly(2026, 2, 15);
            var batch = Batch.Create(batchId, prodId, batchNumber, manufacturedOn).Value;

            var batchComponentId = BatchComponentId.NewBatchComponentId();
            var massInGrams = MassInGrams.Create(0.45f).Value;
            var rawMaterialLotId = RawMaterialLotId.NewRawMaterialLotId();
            var batchComponent = BatchComponent.Create(batchComponentId, massInGrams, rawMaterialLotId).Value;
            
            var resultAdd = batch.AddComponent(batchComponent);
            Assert.True(resultAdd.IsSuccess);

            var targetId = useExitingId ? batchComponentId : BatchComponentId.NewBatchComponentId();
            var newMass = provideNewMass ? MassInGrams.Create(0.75f).Value : null;

            var resultChange = batch.ChangeComponentMass(targetId, newMass!);
            Assert.Equal(expectedSuccess, resultChange.IsSuccess);

            var expectedMass = expectedSuccess == true ? newMass : massInGrams;

            Assert.Equal(expectedMass, batchComponent.Mass);
        }
    }
}
