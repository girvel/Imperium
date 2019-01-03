using System;
using Imperium.Ecs.Attributes;
using Xunit;

namespace Imperium.Ecs.Tests.Attributes
{
    public class BasedOnAttributeTests
    {
        [Fact]
        public void Ctor_ThrowsArgumentExceptionWhenRequiresNotASystem()
        {
            // act
            var thrown = false;
            try
            {
                new BasedOnAttribute(typeof(object));
            }
            catch (ArgumentException e)
            {
                thrown = true;
            }
            
            // assert
            Assert.True(thrown);
        }
    }
}