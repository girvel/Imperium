using Imperium.Core.Systems.Owning;
using Moq;
using Xunit;

namespace Imperium.Core.Tests.Systems.Owning
{
    public class PlayerTests
    {
        [Fact]
        public void AddOwned_AddsOwnedToListAndCallsEvent()
        {
            // arrange
            var eventCalled = false;
            
            var player = new Player();
            player.OnOwnedAdded += o => eventCalled = true;
            
            var owned = Mock.Of<Owned>();
            
            // act
            player.AddOwned(owned);
            
            // assert
            Assert.True(eventCalled);
            Assert.True(player.OwnedSubjects.Contains(owned));
        }
        
        [Fact]
        public void RemoveOwned_RemovesOwnedFromListAndCallsEvent()
        {
            // arrange
            var eventCalled = false;
            
            var player = new Player();
            player.OnOwnedRemoved += o => eventCalled = true;
            
            var owned = Mock.Of<Owned>();
            player.OwnedSubjects.Add(owned);
            
            // act
            player.RemoveOwned(owned);
            
            // assert
            Assert.True(eventCalled);
            Assert.False(player.OwnedSubjects.Contains(owned));
        }
    }
}