using Imperium.Core.Systems.Owning;
using Imperium.Ecs.Managers;
using Moq;
using Xunit;

namespace Imperium.Core.Tests.Systems.Owning
{
    public class PlayerComponentTests
    {
        [Fact]
        public void Owner_Set_MovesToANewPlayerList()
        {
            // arrange
            var newPlayer = Mock.Of<Player>();
            var system = Mock.Of<PlayerSystem>();
            system.Players.Add(newPlayer);

            var ecs = Mock.Of<EcsManager>();
            ecs.SystemManager = Mock.Of<SystemManager>(s => s.GetSystem<PlayerSystem>() == system);
            
            // ReSharper disable once UseObjectOrCollectionInitializer
            var component = new OwnedComponent{Ecs = ecs};
            
            // act
            component.Owner = newPlayer;
            
            // assert
            Assert.Equal(component, newPlayer.OwnedSubjects[0]);
        }

        [Fact]
        public void Owner_Set_SafeForNullReferenceExceptions()
        {
            // arrange
            var system = Mock.Of<PlayerSystem>();
            
            // ReSharper disable once UseObjectOrCollectionInitializer
            var component = new OwnedComponent();
            
            // act
            component.Owner = null;
            
            // assert
            Assert.True(true);
        }
    }
}