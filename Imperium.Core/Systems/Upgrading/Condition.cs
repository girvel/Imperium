using System.Linq;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Upgrading.Conditions;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading
{
    public abstract class Condition
    {
        public static AndCondition operator &(Condition c1, Condition c2)
            => new AndCondition(
                ((c1 as AndCondition)?.Conditions ?? new[] {c1}).Concat((c2 as AndCondition)?.Conditions ?? new[] {c2})
                .ToArray());
        
        public abstract bool IsPossible(Entity from, Owner owner);

        public abstract void Apply(Entity from, Owner owner);
    }
}