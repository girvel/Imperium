using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Income
{
    public class IncomeSystem : RegistrationSystem<Incomer>
    {
        public override void Update()
        {
            base.Update();

            foreach (var resourceBuilding in Subjects)
            {
                var owner = resourceBuilding.Parent.GetComponent<Owned>()?.Owner;

                if (owner != null)
                {
                    owner.Resources 
                        = owner.Resources.Added(
                            resourceBuilding.Income.Multiplied(
                                Time.GameSecondToUpdateCoefficient(Ecs) / 3600 / 24));
                }
            }
        }
    }
}