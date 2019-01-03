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

        public List<Owned> OwnedSubjects { get; set; } = new List<Owned>();

        
        
        public event Action<IResources> OnResourcesChanged;
        public event Action<Owned> OnOwnedAdded, OnOwnedRemoved;
        private IResources _resources;



        public virtual void AddOwned(Owned item)
        {
            OwnedSubjects.Add(item);
            OnOwnedAdded?.Invoke(item);
        }

        public virtual bool RemoveOwned(Owned item)
        {
            var success = OwnedSubjects.Remove(item);
            
            if (success) OnOwnedRemoved?.Invoke(item);

            return success;
        }
    }
}