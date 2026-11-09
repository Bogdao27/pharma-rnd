namespace PharmaRnd.Domain.Tests
{
    public class BatchNumberTests
    {
        [Fact]
        public void Create_BatchNumber_ReturnsTrue()
        {
            var result = BatchNumber.Create("010126");

            Assert.True(result.IsSuccess);
            Assert.Equal("010126", result.Value.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("0001267")]
        [InlineData("")]
        [InlineData("01020")]
        [InlineData("010F26")]
        [InlineData("000126")]
        [InlineData("010026")]
        [InlineData("011326")]
        public void Create_WithInvalidValue_ReturnsFailure(string? value)
        {
            var result = BatchNumber.Create(value!);

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void BatchNumbers_WithSameValues_AreEqual()
        {
            var first = BatchNumber.Create("010126");
            var second = BatchNumber.Create("010126");

            Assert.Equal(first.Value, second.Value);
        }

        [Fact]
        public void BatchNumbers_WithDifferentValues_AreEqual()
        {
            var first = BatchNumber.Create("010126");
            var second = BatchNumber.Create("010127");

            Assert.NotEqual(first.Value, second.Value);
        }

        [Theory]
        [InlineData( //отличается только день
            "121126",
            2026,
            11,
            14,
            true
            )]
        [InlineData( //отличается месяц
            "121126",
            2026,
            12,
            14,
            false
            )]
        [InlineData( //отличается год
            "121126",
            2027,
            11,
            14,
            false
            )]
        [InlineData( //отличается месяц и год
            "121126",
            2027,
            12,
            14,
            false
            )]
        public void MatchesManufacturedOn_ReturnsExpetedResult(string number, int year, int month, int day, bool expected)
        {
            var batchNumber = BatchNumber.Create(number).Value;
            var dateOnly = new DateOnly(year, month, day);

            var actual = batchNumber.MatchesManufacturedOn(dateOnly);

            Assert.Equal(expected, actual);
        } 
    }
}
