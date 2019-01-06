using Xunit;

namespace Province.Vector.Tests
{
    public class VectorTests
    {
        [Fact]
        public void Parse_ReturnsTrueAndSetsResultArgumentWhenSourceIsValid()
        {
            // arrange
            var source = "{+1; -2}";
            Vector result;
            
            // act
            var success = Vector.TryParse(source, out result);
            
            // assert
            Assert.True(success);
            Assert.Equal(new Vector(1, -2), result);
        }

        [Fact]
        public void Parse_ReturnsFalseAndSetsResultByDefaultValueWhenSourceIsInvalid()
        {
            // arrange
            var source = "{+1; -";
            Vector result;
            
            // act
            var success = Vector.TryParse(source, out result);
            
            // assert
            Assert.False(success);
            Assert.Equal(default(Vector), result);
        }
    }
}