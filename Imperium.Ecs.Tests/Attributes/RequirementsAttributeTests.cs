using System;
using Imperium.Ecs.Attributes;
using Xunit;

namespace Imperium.Ecs.Tests.Attributes
{
    public class RequirementsAttributeTests
    {
        [Fact]
        public void CheckRequirements_ThrowsExceptionIfThereAreSomeOutstandingRequirements()
        {
            var exceptionThrown = false;
            
            // arrange
            var a = new A();

            // act
            try
            {
                RequirementsAttribute.CheckRequirements(a);
            }
            catch (RequirementsException)
            {
                exceptionThrown = true;
            }
            
            // assert
            Assert.True(exceptionThrown);
        }


        
        [TestRequirements(typeof(bool), typeof(float))]
        private class A
        {
            
        }
        
        private class TestRequirementsAttribute : RequirementsAttribute
        {
            public TestRequirementsAttribute(params Type[] types) : base(types, typeof(object))
            {
            }

            protected override Type[] _getCheckingTypes(object o)
            {
                return new[] {typeof(bool), typeof(string), typeof(int)};
            }
        }
    }
}