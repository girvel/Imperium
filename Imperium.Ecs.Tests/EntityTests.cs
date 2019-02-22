using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Imperium.Ecs.Attributes;
using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests
{
    public class EntityTests
    {
        [Fact]
        public void AddComponent_AddsComponentToListsAndGivesHimReferencesAndRegistersIt()
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
            
            Assert.Equal(entity.Ecs, component.Object.Ecs);
            Assert.Equal(entity, component.Object.Parent);
        }

        [Fact]
        public void RemoveComponent_RemovesComponentAndUnregistersIt()
        {
            // arrange
            var component = new Mock<Component>();
            
            var componentManager = new Mock<ComponentManager>();
                
            var ecs = new Mock<EcsManager>();
            ecs.SetupGet(e => e.ComponentManager).Returns(componentManager.Object);
            
            var entity = new Entity
            {
                Ecs = ecs.Object,
                Components = {component.Object},
            };
            
            // act
            entity.RemoveComponent(component.Object);
            
            // assert
            componentManager.Verify(cm => cm.Unregister(component.Object), Times.Once);
            
            Assert.False(entity.Components.Contains(component.Object));
        }

        [Fact]
        public void GetComponent_ReturnsComponentOfType()
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

        [Fact]
        public void GetComponent_ReturnsNullWhenThereIsNoComponentOfType()
        {
            // arrange
            var entity = new Entity();
            
            // act
            var result = entity.GetComponent<TestComponent>();
            
            // assert
            Assert.Null(result);
        }

        [Fact]
        public void OperatorOr_ChecksComponentRequirements()
        {
            // arrange
            var entity = new Entity();
            var component = new TestedComponent();

            var exceptionThrown = false;
            
            // act
            try
            {
                entity |= component;
            }
            catch (RequirementsException e)
            {
                exceptionThrown = true;
            }
            
            // assert
            Assert.True(exceptionThrown);
        }

        [Fact]
        public void OperatorMore_ChecksIsTheEntityCloneOfPrototype()
        {
            // arrange
            var prototype = new Entity();
            var entityClone = new Entity {Prototype = prototype};
            var justEntity = new Entity();
            
            // assert
            Assert.True(entityClone < prototype);
            Assert.False(justEntity < prototype);
        }

        [Fact]
        public void OperatorLess_IsReversedMoreOperator()
        {
            // arrange
            var prototype = new Entity();
            var entityClone = new Entity {Prototype = prototype};
            var justEntity = new Entity();
            
            // assert
            Assert.True(prototype > entityClone);
            Assert.False(prototype > justEntity);
        }

        
        
        private class RequiredComponent : Component
        {
            
        }

        [RequiresComponents(typeof(RequiredComponent))]
        private class TestedComponent : Component
        {
            
        }

        private class TestComponent : Component
        {
            
        }
    }
}