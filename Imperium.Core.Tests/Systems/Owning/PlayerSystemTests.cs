using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Moq;
using Xunit;

namespace Imperium.Core.Tests.Systems.Owning
{
    public class PlayerSystemTests
    {
        [Fact]
        public void Register_AddsPlayerToListAndSetHisResourcesAndCallsEvent()
        {
            // arrange

            var eventCalled = false;
            var startingResources = new InternalResources();
            
#pragma warning disable 618
            var playerSystem = new Ownership{PlayerStartingResources = startingResources};
#pragma warning restore 618
            playerSystem.OnPlayerCreated += p => eventCalled = true;

            var player = Mock.Of<Owner>();
            
            // act
            playerSystem.Register(player);
            
            // assert
            Assert.True(playerSystem.Subjects.Contains(player));
            Assert.Equal(startingResources, player.Resources);
            Assert.True(eventCalled);
        }
    }
}