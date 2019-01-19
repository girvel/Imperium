using System;
using System.Linq;

namespace Imperium.Ecs.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiresSystemsAttribute : RequirementsAttribute
    {
        public RequiresSystemsAttribute(params Type[] requiredSystems) : base(requiredSystems, typeof(System))
        {
        }

        protected override Type[] _getCheckingTypes(object o)
        {
            return ((System) o).Ecs.SystemManager.Subjects.Select(s => s.GetType()).ToArray();
        }
    }
}