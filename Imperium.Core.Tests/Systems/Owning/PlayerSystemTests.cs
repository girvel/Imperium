﻿using Imperium.Core.Common;
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
            var startingResources = Mock.Of<IResources>(r => r.Clone() == r);
            
#pragma warning disable 618
            var playerSystem = new PlayerSystem{PlayerStartingResources = startingResources};
#pragma warning restore 618
            playerSystem.OnPlayerCreated += p => eventCalled = true;

            var player = Mock.Of<Player>();
            
            // act
            playerSystem.Register(player);
            
            // assert
            Assert.True(playerSystem.Players.Contains(player));
            Assert.Equal(startingResources, player.Resources);
            Assert.True(eventCalled);
        }
    }
}