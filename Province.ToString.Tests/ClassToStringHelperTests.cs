using System;
using System.Linq;
using Xunit;

namespace Province.ToString.Tests
{
    public class ClassToStringHelperTests
    {
        [Fact]
        public void ConvertsToStringByConcreteFormat()
        {
            // arrange
            var dummy = new Dummy {A = 10, B = 5, D = new object[] {true, "false"}};
            
            // act
            var str = dummy.ToString();
            
            // assert
            Assert.True(str.StartsWith("[Dummy | "));
            Assert.True(str.EndsWith("]"));
            
            var components = new[]
            {
                "A: 10",
                "B: 5",
                "C: \"H\"",
                "D: {True, \"false\"}",
            };

            foreach (var component in components)
            {
                Assert.Contains(component, str);
            }
        }

        private class Dummy
        {
            [Representative]
            public int A, B;

            [Representative]
            public string C => "H";

            [Representative] public object[] D;
            
            public override string ToString() => this.ToRepresentativeString();
        }
    }
}