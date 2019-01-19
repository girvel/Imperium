using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Imperium.Ecs.Attributes
{
    [Serializable]
    public class RequirementsException : Exception
    {
        public object SourceObject { get; }
            
        public Type[] OutstandingRequirements { get; set; }
            
            
            
        public RequirementsException()
        {
        }
            
        public RequirementsException(object sourceObject, Type[] outstandingRequirements) 
            : base($"The {sourceObject.GetType().Name} {sourceObject} can not find {sourceObject.GetType().Name}s " 
                   + outstandingRequirements.Aggregate("", (sum, t) => sum + ", " + t.Name).Substring(2))
        {
            SourceObject = sourceObject;
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