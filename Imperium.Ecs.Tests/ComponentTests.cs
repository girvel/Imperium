using System.Collections.Generic;
using Imperium.Ecs.Attributes;
using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests
{
    public class ComponentTests
    {
        [Fact]
        public void Clone_ReturnsCloneOfComponentAndSetsPrototype()
        {
            // arrange
            var component = new Component
            {
                Ecs = Mock.Of<EcsManager>(),
            };
            
            // act
            var clone = (Component) component.Clone();
            
            // assert
            Assert.Equal(clone.Ecs, component.Ecs);
            Assert.Equal(component, clone.Prototype);
            Assert.NotEqual(component, clone);
        }

        [Fact]
        public void Start_ChecksRequirements()
        {
            // arrange
            var component = new TestingComponent
            {
                Parent = Mock.Of<Entity>(),
            };

            var exceptionThrown = false;
            
            // act
            try
            {
                component.Start();
            }
            catch (RequirementsException e)
            {
                exceptionThrown = true;
            }
            
            // assert
            Assert.True(exceptionThrown);
        }

        private class RequiredComponent : Component
        {
            
        }

        [RequiresComponents(typeof(RequiredComponent))]
        private class TestingComponent : Component
        {
            
        }
    }
}