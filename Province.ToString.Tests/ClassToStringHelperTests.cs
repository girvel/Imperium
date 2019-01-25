using System;
using Xunit;

namespace Province.ToString.Tests
{
    public class ClassToStringHelperTests
    {
        [Fact]
        public void ConvertsToStringByConcreteFormat()
        {
            // arrange
            var dummy = new Dummy {A = 10, B = 5};
            
            // act
            var str = dummy.ToString();
            
            // assert
            Assert.Equal("[Dummy | A: 10, B: 5, C: \"H\"]", str);
        }

        private class Dummy
        {
            [Representative]
            public int A, B;

            [Representative]
            public string C => "H";
            
            public override string ToString() => this.ToRepresentativeString();
        }
    }
}