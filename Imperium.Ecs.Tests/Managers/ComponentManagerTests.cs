using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests.Managers
{
    public class ComponentManagerTests
    {
        [Fact]
        public void Register_AddsComponentToListAndStartsIt()
        {
            // arrange
            var manager = new ComponentManager();
            var component = new Mock<Component>();

            // act
            manager.Register(component.Object);
            
            // assert
            Assert.Contains(component.Object, manager.Subjects);
            component.Verify(c => c.Start(), Times.Once);
        }
        
        [Fact]
        public void Unregister_RemovesComponentToListAndDestroysIt()
        {
            // arrange
            var component = new Mock<Component>();
            var manager = new ComponentManager{Subjects = {component.Object}};

            // act
            manager.Unregister(component.Object);
            
            // assert
            Assert.DoesNotContain(component.Object, manager.Subjects);
            component.Verify(c => c.Destroy(), Times.Once);
        }
    }
}