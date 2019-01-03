using Imperium.Core.Common;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Income
{
    public class Incomer : RegisteredComponent<IncomeSystem, Incomer>
    {
        public IResources Income { get; set; }
    }
}