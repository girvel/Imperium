using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests
{
    public class ComponentTests
    {
        [Fact]
        public void Clone_ReturnsCloneOfComponent()
        {
            // arrange
            var component = new Component
            {
                Ecs = Mock.Of<EcsManager>(),
            };
            
            // act
            var clone = (Component) component.Clone();
            
            // assert
            Assert.Equal(component.Ecs, clone.Ecs);
        }
    }
}