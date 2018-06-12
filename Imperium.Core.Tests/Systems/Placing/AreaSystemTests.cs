using System.Linq;
using Imperium.Core.Common;
using Imperium.Core.Systems.Placing;
using Moq;
using Xunit;

namespace Imperium.Core.Tests.Systems.Placing
{
    public class AreaSystemTests
    {
        [Fact]
        public void Move_MovesComponentToNewPosition()
        {
            // arrange
            var system = new AreaSystem(new Area(new Vector(2, 1)));
            var component = Mock.Of<PositionComponent>();
            system.Area.Grid[0, 0].Add(component);
            
            // act
            system.Move(component, new Vector(1, 0));
            
            // assert
            Assert.False(system.Area.Grid[0, 0].Any());
            Assert.True(system.Area.Grid[1, 0].Contains(component));
            Assert.Equal(new Vector(1, 0), component.Position);
        }
    }
}