using System;
using System.Collections.Generic;
using Imperium.Core.Common;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Owning
{
    public class Ownership : RegistrationSystem<Owner>
    {
        public IResources PlayerStartingResources { get; set; }



        [Obsolete("testing ctor")]
        public Ownership()
        {
        }

        public Ownership(IResources playerStartingResources)
        {
            PlayerStartingResources = playerStartingResources;
        }



        public override void Register(Owner owner)
        {
            base.Register(owner);
            
            owner.Resources = (IResources) PlayerStartingResources.Clone();
            OnPlayerCreated?.Invoke(owner);
        }



        public event Action<Owner> OnPlayerCreated;
    }
}