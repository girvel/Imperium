using System;
using System.ComponentModel;
using System.Linq;
using Imperium.Core.Common;
using Imperium.Core.Systems.Placing;
using Moq;
using Xunit;

namespace Imperium.Core.Tests
{
    public class Tests
    {
        [Fact]
        public void Move_MovesComponentToNewPosition()
        {
            // arrange
            var system = new AreaSystem(new Vector(2, 1));
            var component = Mock.Of<PositionComponent>();
            system.Area[0, 0].Add(component);
            
            // act
            system.Move(component, new Vector(1, 0));
            
            // assert
            Assert.False(system.Area[0, 0].Any());
            Assert.True(system.Area[0, 1].Contains(component));
            Assert.Equal(new Vector(1, 0), component.Position);
        }
    }
}