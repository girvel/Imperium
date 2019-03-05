using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Income
{
    public class IncomeSystem : ParallelRegistrationSystem<Incomer>
    {
        public override void UpdateSubject(Incomer resourceBuilding)
        {
            var owner = resourceBuilding.Parent.GetComponent<Owned>()?.Owner;

            if (owner != null)
            {
                owner.Resources += resourceBuilding.Income * (float) Ecs.UpdateDelay.TotalSeconds;
            }
        }
    }
}