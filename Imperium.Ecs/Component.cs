using System;
using Imperium.Ecs.Managers;

namespace Imperium.Ecs
{
    public class Component : ICloneable
    {
        public Entity Parent { get; set; }
        
        public EcsManager Ecs { get; set; }
        
        
        
        public Component Prototype { get; set; }
        
        
        
        public virtual void Start()
        {
        }

        public virtual void Destroy()
        {
        }

        public virtual object Clone()
        {
            return (Component) MemberwiseClone();
        }

        public override string ToString() => $"[{GetType().Name}]";
    }
}