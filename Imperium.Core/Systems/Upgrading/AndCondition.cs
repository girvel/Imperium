using System.Linq;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading
{
    public class AndCondition : Condition
    {
        public Condition[] Conditions { get; }

        public AndCondition()
        {
        }
        
        public AndCondition(params Condition[] conditions)
        {
            Conditions = conditions;
        }


        public override bool IsPossible(Entity @from, Owner owner)
        {
            return Conditions.All(c => c.IsPossible(@from, owner));
        }

        public override void Apply(Entity @from, Owner owner)
        {
            foreach (var condition in Conditions)
            {
                condition.Apply(from, owner);
            }
        }

        public override string ToString() => $"({Conditions.Aggregate("", (sum, c) => sum + " & " + c).Substring(3)})";
    }
}