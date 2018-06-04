using System.Linq;
using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Ecs.Tests.Managers
{
    public class SystemManagerTests
    {
        [Fact]
        public void Update_ShouldCallAllSystemsUpdates()
        {
            // arrange
            var systems = new[]
            {
                new Mock<ISystem>(),
                new Mock<ISystem>(),
                new Mock<ISystem>(),
            };

            var manager = new SystemManager
            {
                Subjects = systems.Select(s => s.Object).ToList(),
            };
            
            // act
            manager.Update();
            
            // assert
            foreach (var system in systems)
            {
                system.Verify(s => s.Update());
            }
        }

        [Fact]
        public void GetSystem_ShouldReturnSystemOfType()
        {
            // arrange
            var system = new TestSystem();
            var manager = new SystemManager
            {
                Subjects = {system, Mock.Of<ISystem>()}
            };
            
            // act
            var result = manager.GetSystem<TestSystem>();
            
            // assert
            Assert.Equal(system, result);
        }

        class TestSystem : ISystem
        {
            public void Update()
            {
            }
        }
    }
}