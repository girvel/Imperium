using Xunit;

namespace Province.Vector.Tests
{
    public class ArrayHelperTests
    {
        [Fact]
        public void FindNearest_FindsNearestElementForOptimalTime()
        {
            // arrange
            var callCounter = 0;
            
            var basicArray = new[,]
            {
                {01, 02, 03, 04, 05, 06},
                {11, 12, 13, 14, 15, 16},
                {21, 22, 23, 24, 25, 26},
                {31, 32, 33, 34, 35, 36},
                {41, 42, 43, 44, 45, 46},
                {51, 52, 53, 54, 55, 56},
            };
            
            // act
            var result = basicArray.FindNearest(
                new Vector(3, 3), 
                item =>
                {
                    callCounter++;
                    return item == 56;
                });
            
            // assert
            Assert.Equal(56, result);
            Assert.True(callCounter <= 25);
        }
    }
}
