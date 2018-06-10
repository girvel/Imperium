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
            // arrange
            var manager = new EntityManager
            {
                Ecs = Mock.Of<EcsManager>(),
            };
            
            // act
            var entity = manager.CreateNew();
            
            // assert
            Assert.Equal(manager.Ecs, entity.Ecs);
            Assert.True(manager.Entities.Contains(entity));
        }

        [Fact]
        public void CreateNew_CanCopyEntities()
        {
            // arrange
            var manager = new EntityManager
            {
                Ecs = Mock.Of<EcsManager>(),
            };
            
            var original = Mock.Of<Entity>();
            original.Name = "Original";
            original.Ecs = manager.Ecs;

            var component = new Mock<Component>();
            component.Setup(c => c.Clone()).Returns(Mock.Of<Component>());
            original.Components.Add(component.Object);
            
            // act
            var copy = manager.CreateNew(original);
            
            // assert
            Assert.Equal(original.Ecs, copy.Ecs);
            Assert.Equal(original.Name, copy.Name);
            Assert.True(manager.Entities.Contains(copy));
            Assert.False(copy.Components.Contains(component.Object));
            component.Verify(c => c.Clone(), Times.Once);
        }
    }
}