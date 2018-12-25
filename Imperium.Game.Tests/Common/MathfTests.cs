using Imperium.Game.Common;
using Xunit;

namespace Imperium.Game.Tests.Common
{
    public class MathfTests
    {
        [Fact]
        public void Max_ReturnsMaximalValueInArray()
        {
            // arrange
            var array = new[] {1, 2, 3};
            
            // act
            var max = Mathf.Max(array);
            
            // assert
            Assert.Equal(3, max);
        }
        
        [Fact]
        public void Min_ReturnsMinimalValueInArray()
        {
            // arrange
            var array = new[] {1, 2, 3};
            
            // act
            var min = Mathf.Min(array);
            
            // assert
            Assert.Equal(1, min);
        }
    }
}