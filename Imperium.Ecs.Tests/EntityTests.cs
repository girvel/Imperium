using System;
using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests
{
    public class EntityTests
    {
        [Fact]
        public void AddComponent_ShouldAddComponentToListsAndGiveHimReferencesAndStartIt()
        {
            // arrange
            var component = new Mock<Component>();
            var componentManager = new Mock<ComponentManager>();
            var ecs = new Mock<EcsManager>();
            ecs.SetupGet(e => e.ComponentManager).Returns(componentManager.Object);
            
            var entity = new Entity
            {
                Ecs = ecs.Object,
            };
            
            // act
            entity.AddComponent(component.Object);
            
            // assert
            Assert.True(entity.Components.Contains(component.Object));
            
            componentManager.Verify(c => c.Register(component.Object), Times.Once);
            component.Verify(c => c.Start());
            
            Assert.Equal(entity.Ecs, component.Object.Ecs);
            Assert.Equal(entity, component.Object.Parent);
        }

        [Fact]
        public void GetComponent_ShouldReturnComponentOfType()
        {
            // arrange
            var component = new TestComponent();
            var entity = new Entity
            {
                Components = {component},
            };
            
            // act
            var result = entity.GetComponent<TestComponent>();
            
            // assert
            Assert.Equal(component, result);
        }

        class TestComponent : Component
        {
            
        }
    }
}