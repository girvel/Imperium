using System;
using System.Linq;

namespace Imperium.Ecs.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BasedOnAttribute : Attribute
    {
        public Type[] RequiredSystems { get; }

        public BasedOnAttribute(params Type[] requiredSystems)
        {
            if (requiredSystems.Any(t => !t.IsSubclassOf(typeof(System)))) 
                throw new ArgumentException("System can only require subclasses of System");
            
            RequiredSystems = requiredSystems;
        }
    }
}