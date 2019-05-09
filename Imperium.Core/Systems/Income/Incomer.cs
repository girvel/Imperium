using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Income
{
    public class Incomer : RegisteredComponent<Incomer>
    {
        public InternalResources IncomePerSecond { get; set; }

        public override void Update()
        {
            base.Update();
            
            var owner = Parent.GetComponent<Owned>()?.Owner;

            if (owner != null)
            {
                owner.Resources += IncomePerSecond * (float) Ecs.UpdateDelay.TotalSeconds;
            }
        }
    }
}