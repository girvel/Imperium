using System.Collections.Generic;
using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;

namespace Imperium.Core.Systems.Income
{
    public class IncomeSystem : Ecs.System
    {
        public List<Incomer> Subjects = new List<Incomer>();

        public void Register(Incomer subject)
        {
            Subjects.Add(subject);
        }

        public override void Update()
        {
            base.Update();

            foreach (var resourceBuilding in Subjects)
            {
                var owner = resourceBuilding.Parent.GetComponent<OwnedComponent>()?.Owner;

                if (owner != null)
                {
                    owner.Resources 
                        = owner.Resources.Added(
                            resourceBuilding.Income.Multiplied(
                                Time.UpdateGameTimeCoefficient(Ecs)));
                }
            }
        }
    }
}