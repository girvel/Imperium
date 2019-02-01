using System.Collections.Generic;
using Imperium.Core.Systems.Execution;
using Imperium.Core.Systems.Fight;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Attributes;

namespace Imperium.Core.Systems.Ai
{
    [RequiresComponents(typeof(Executor), typeof(Fighter))]
    public class AutoAttack : Component
    {
        //public List<Placer> 
    }
}