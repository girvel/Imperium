using System;
using Imperium.Ecs.Managers;

namespace Imperium.Ecs
{
    public class Component : ICloneable
    {
        public Entity Parent { get; set; }
        
        public EcsManager Ecs { get; set; }
        
        
        
        public virtual void Start()
        {
        }

        public virtual object Clone()
        {
            var clone = (Component) MemberwiseClone();
            Ecs.ComponentManager.Register(clone);
            return clone;
        }
    }
}