using System;
using System.Linq;
using System.Runtime.Serialization;
using Imperium.Ecs.Attributes;
using Imperium.Ecs.Managers;
using Province.ToString;

namespace Imperium.Ecs
{
    public abstract class System
    {
        public EcsManager Ecs { get; set; }



        public virtual void Start()
        {
            RequirementsAttribute.CheckRequirements(this);
        }
        
        public virtual void Update()
        {
            
        }

        

        public override string ToString() => this.ToRepresentativeString();
    }
}