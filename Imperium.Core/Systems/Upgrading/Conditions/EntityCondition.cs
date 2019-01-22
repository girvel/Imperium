using System.Linq;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading.Conditions
{
    public class EntityCondition : Condition
    {
        public Entity Prototype { get; }
        
        public EntityCondition(Entity prototype)
        {
            Prototype = prototype;
        }

        public override bool IsPossible(Entity @from, Owner owner)
        {
            return from.Ecs
                .GetSystem<Area>()[from.GetComponent<Placer>().Position]
                .Any(p => p.Parent < Prototype);
        }

        public override void Apply(Entity @from, Owner owner)
        {
            
        }
    }
}