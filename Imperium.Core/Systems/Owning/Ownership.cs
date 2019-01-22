using System;
using System.Collections.Generic;
using Imperium.Core.Common;

namespace Imperium.Core.Systems.Owning
{
    public class Ownership : Ecs.System
    {
        public List<Owner> Players { get; set; } = new List<Owner>();
        
        public IResources PlayerStartingResources { get; set; }



        [Obsolete("testing ctor")]
        public Ownership()
        {
        }

        public Ownership(IResources playerStartingResources)
        {
            PlayerStartingResources = playerStartingResources;
        }



        public void Register(Owner owner)
        {
            Players.Add(owner);
            owner.Resources = (IResources) PlayerStartingResources.Clone();
            OnPlayerCreated?.Invoke(owner);
        }



        public event Action<Owner> OnPlayerCreated;
    }
}