using System;
using System.Collections.Generic;
using Imperium.Core.Common;

namespace Imperium.Core.Systems.Owning
{
    public class Player
    {
        public string Name { get; set; }

        public IResources Resources
        {
            get => _resources;
            set
            {
                _resources = value; 
                OnResourcesChanged?.Invoke(_resources);
            }
        }

        public List<OwnedComponent> OwnedSubjects { get; set; } = new List<OwnedComponent>();

        
        
        public event Action<IResources> OnResourcesChanged;
        private IResources _resources;
    }
}