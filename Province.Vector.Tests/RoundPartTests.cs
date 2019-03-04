using System.Linq;
using Xunit;

namespace Province.Vector.Tests
{
    public class RoundPartTests
    {
        [Fact]
        public void GetEnumerator_ReturnsEnumeratorForAllItemsInRound()
        {
            // arrange
            var basicArray = new[,]
            {
                {01, 02, 03, 04, 05, 06},
                {11, 12, 13, 14, 15, 16},
                {21, 22, 23, 24, 25, 26},
                {31, 32, 33, 34, 35, 36},
                {41, 42, 43, 44, 45, 46},
                {51, 52, 53, 54, 55, 56},
            };
            
            var round = new RoundPart<int>(basicArray, new Vector(2, 1), 2.3);
            
            // act
            var collection = round.ToArray();
            
            // assert
            var expectation = new[] {01, 02, 03, 11, 12, 13, 14, 21, 22, 23, 24, 31, 32, 33, 34, 41, 42, 43};
            
            Assert.True(expectation.All(n => collection.Contains(n)));
            Assert.Equal(expectation.Length, collection.Length);
        }
    }
}
