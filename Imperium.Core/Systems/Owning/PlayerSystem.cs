using System;
using System.Collections.Generic;
using Imperium.Core.Common;

namespace Imperium.Core.Systems.Owning
{
    public class PlayerSystem : Ecs.System
    {
        public List<Player> Players { get; set; } = new List<Player>();
        
        public IResources PlayerStartingResources { get; set; }



        public PlayerSystem(IResources playerStartingResources)
        {
            PlayerStartingResources = playerStartingResources;
        }



        public void Register(Player player)
        {
            Players.Add(player);
            player.Resources = (IResources) PlayerStartingResources.Clone();
            OnPlayerCreated?.Invoke(player);
        }



        public event Action<Player> OnPlayerCreated;
    }
}