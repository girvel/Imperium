﻿using System;
using System.Linq;
using Province.ToString;

namespace Imperium.Ecs.Attributes
{
    public abstract class RequirementsAttribute : Attribute
    {
        [Representative]
        public Type[] Requirements { get; set; }

        protected RequirementsAttribute()
        {
        }

        protected RequirementsAttribute(Type[] requirements, Type baseType)
        {
            if (requirements.Any(t => !t.IsSubclassOf(baseType))) 
                throw new ArgumentException($"Requirements can be only of type {baseType.Name}");
            
            Requirements = requirements;
        }

        public static void CheckRequirements(object o)
        {
            var attribute 
                = o.GetType()
                    .GetCustomAttributes(true)
                    .OfType<RequirementsAttribute>()
                    .FirstOrDefault();

            if (attribute != null)
            {
                var checkingTypes = attribute._getCheckingTypes(o);
                
                var outstandingRequirements
                    = attribute.Requirements
                        .Where(t => !checkingTypes.Any(s => s.IsSubclassOf(t) || t == s))
                        .ToArray();
                
                if (outstandingRequirements.Any())
                {
                    throw new RequirementsException(o, outstandingRequirements);
                }
            }
        }

        protected abstract Type[] _getCheckingTypes(object o);
        
        
        
        public override string ToString() => this.ToRepresentativeString();
    }
}