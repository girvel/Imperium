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
    }
}