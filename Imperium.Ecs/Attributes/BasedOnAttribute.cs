using System;

namespace Imperium.Ecs.Attributes
{
    public class BasedOnAttribute : Attribute
    {
        public Type[] RequiredSystems { get; }

        public BasedOnAttribute(params Type[] requiredSystems)
        {
            RequiredSystems = requiredSystems;
        }
    }
}