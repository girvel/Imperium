using Imperium.Core.Common;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Income
{
    public class Incomer : Component
    {
        public IResources Income { get; set; }
        
        public override void Start()
        {
            base.Start();
            
            Ecs.SystemManager.GetSystem<IncomeSystem>().Register(this);
        }
    }
}