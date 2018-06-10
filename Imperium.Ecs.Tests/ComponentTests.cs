using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests
{
    public class ComponentTests
    {
        [Fact]
        public void Clone_ReturnsCloneOfComponentAndRegistersIt()
        {
            // arrange
            var componentManager = new Mock<ComponentManager>();
            var ecs = Mock.Of<EcsManager>();
            ecs.ComponentManager = componentManager.Object;

            var component = new Component
            {
                Ecs = ecs,
            };
            
            // act
            var clone = (Component) component.Clone();
            
            // assert
            Assert.Equal(component.Ecs, clone.Ecs);
            componentManager.Verify(c => c.Register(clone), Times.Once);
        }
    }
}