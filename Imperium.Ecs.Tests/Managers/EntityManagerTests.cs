using System.Collections.Generic;
using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests.Managers
{
    public class EntityManagerTests
    {
        [Fact]
        public void CreateNew_ShouldCreateNewEntityWithEcsReferenceAndAddItToList()
        {
            var isEventCalled = false;
            
            // arrange
            var manager = new EntityManager
            {
                Ecs = Mock.Of<EcsManager>(),
            };
            
            manager.OnEntityCreate += e => isEventCalled = true;
            
            // act
            var entity = manager.Create();
            
            // assert
            Assert.Equal(manager.Ecs, entity.Ecs);
            Assert.True(manager.Entities.Contains(entity));
            Assert.True(isEventCalled, "Create event should be called");
        }

        [Fact]
        public void CreateNew_CanCopyEntities()
        {
            // arrange
            var manager = new EntityManager
            {
                Ecs = Mock.Of<EcsManager>(),
            };

            var componentManagerMock = new Mock<ComponentManager>();

            manager.Ecs.ComponentManager = componentManagerMock.Object;
            
            var original = Mock.Of<Entity>();
            original.Name = "Original";
            original.Ecs = manager.Ecs;

            var component = new Mock<Component>();
            component.Setup(c => c.Clone()).Returns(Mock.Of<Component>());
            original.Components.Add(component.Object);
            
            // act
            var copy = manager.Create(original);
            
            // assert
            Assert.Equal(original.Ecs, copy.Ecs);
            Assert.Equal(original.Name, copy.Name);
            Assert.True(manager.Entities.Contains(copy));
            Assert.False(copy.Components.Contains(component.Object));
            component.Verify(c => c.Clone(), Times.Once);
        }

        [Fact]
        public void Destroy_UnregistersComponentsAndRemovesEntity()
        {
            var isEventCalled = false;
            
            // arrange
            var componentManager = new Mock<ComponentManager>();
            var ecs = Mock.Of<EcsManager>(e => e.ComponentManager == componentManager.Object);

            var component = Mock.Of<Component>();
            var entity = Mock.Of<Entity>(e => e.Components == new List<Component>{component} && e.Ecs == ecs);
            
            var manager = new EntityManager
            {
                Ecs = ecs,
                Entities = {entity},
            };
            
            manager.OnEntityDestroy += e => isEventCalled = true;
            
            // act
            manager.Destroy(entity);
            
            // assert
            componentManager.Verify(cm => cm.Unregister(component), Times.Once);
            Assert.False(manager.Entities.Contains(entity));
            Assert.True(isEventCalled, "Destroy event should be called");
        }
    }
}