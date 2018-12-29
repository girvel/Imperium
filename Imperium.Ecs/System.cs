using System;
using System.Linq;
using System.Runtime.Serialization;
using Imperium.Ecs.Attributes;
using Imperium.Ecs.Managers;

namespace Imperium.Ecs
{
    public abstract class System
    {
        public EcsManager Ecs { get; set; }



        public virtual void Start()
        {
            var attribute =
                GetType().GetCustomAttributes(typeof(BasedOnAttribute), true).FirstOrDefault() as BasedOnAttribute;

            if (attribute != null)
            {
                var outstandingRequirements
                    = attribute.RequiredSystems
                        .Where(t =>
                            !Ecs.SystemManager.Subjects.Any(
                                s => s.GetType().IsSubclassOf(t)
                                     || t == s.GetType()))
                        .ToArray();
                
                if (outstandingRequirements.Any())
                {
                    throw new RequirementsException(this, outstandingRequirements);
                }
            }
        }
        
        public virtual void Update()
        {
            
        }

        

        public override string ToString() => $"[{GetType().Name}]";

        

        [Serializable]
        public class RequirementsException : Exception
        {
            public System SourceSystem { get; }
            
            public Type[] OutstandingRequirements { get; set; }
            
            
            
            public RequirementsException()
            {
            }
            
            public RequirementsException(System sourceSystem, Type[] outstandingRequirements) 
                : base($"The system {sourceSystem} can not find systems " 
                       + outstandingRequirements.Aggregate("", (sum, t) => sum + ", " + t.Name).Substring(2))
            {
                SourceSystem = sourceSystem;
                OutstandingRequirements = outstandingRequirements;
            }

            public RequirementsException(string message) : base(message)
            {
            }

            public RequirementsException(string message, Exception inner) : base(message, inner)
            {
            }

            protected RequirementsException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}