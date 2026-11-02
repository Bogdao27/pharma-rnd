namespace PharmaRnd.Domain.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var result = MassInGrams.Create(100);
            
            Assert.True(result.IsSuccess);
            Assert.Equal(100f, result.Value.Value);
        }

        [Theory]
        [InlineData(0f)]
        [InlineData(-1f)]
        public void Create_WithNonPositiveMass_ReturnsFailure(float value)
        {
            var result = MassInGrams.Create(value);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Masses_WithSameValue_AreEqual()
        {
            var massObj1 = MassInGrams.Create(100);
            var massObj2 = MassInGrams.Create(100);

            Assert.Equal(massObj1.Value, massObj2.Value);
        }

        [Fact]
        public void Masses_WithDifferentValues_AreNotEqual()
        {
            var massObj1 = MassInGrams.Create(100);
            var massObj2 = MassInGrams.Create(101);

            Assert.NotEqual(massObj1.Value, massObj2.Value);
        }

        [Theory]
        [InlineData(float.NaN)]
        [InlineData(float.PositiveInfinity)]
        [InlineData(float.NegativeInfinity)]
        public void Create_WithNonFiniteMass_ReturnsFailure(float value)
        {
            var result = MassInGrams.Create(value);

            Assert.True(result.IsFailure);
        }
    }
}
