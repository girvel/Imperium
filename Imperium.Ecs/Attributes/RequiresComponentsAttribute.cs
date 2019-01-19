using System;
using System.Linq;

namespace Imperium.Ecs.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiresComponentsAttribute : RequirementsAttribute
    {
        public RequiresComponentsAttribute(params Type[] requiredComponents) : base(requiredComponents, typeof(Component))
        {
        }

        protected override Type[] _getCheckingTypes(object o)
        {
            return ((Component) o).Parent.Components.Select(c => c.GetType()).ToArray();
        }
    }
}