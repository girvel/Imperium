using System;
using Imperium.Ecs.Attributes;
using Imperium.Ecs.Managers;
using Province.ToString;

namespace Imperium.Ecs
{
    public class Component : ICloneable
    {
        public Entity Parent { get; set; }
        
        public EcsManager Ecs { get; set; }
        
        
        
        public Component Prototype { get; set; }
        
        
        
        public virtual void Start()
        {
            RequirementsAttribute.CheckRequirements(this);
        }

        public virtual void Destroy()
        {
        }

        public virtual Component Clone()
        {
            var result = (Component) MemberwiseClone();
            result.Prototype = this;
            return result;
        }

        object ICloneable.Clone() => Clone();

        public override string ToString() => this.ToRepresentativeString();
    }
}