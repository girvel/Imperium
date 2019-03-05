using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading
{
    public class Upgrade
    {
        public Entity Result { get; set; }
        public Condition Condition { get; }


        public Upgrade(Entity result, Condition condition = null)
        {
            Result = result;
            Condition = condition ?? new NoCondition();
        }

        public bool TryUpgrade(Entity from, Owner owner)
        {
            var success = Condition.IsPossible(from, owner);

            if (success) Condition.Apply(from, owner);

            return success;
        }
        
        private class NoCondition : Condition
        {
            public override bool IsPossible(Entity @from, Owner owner) => true;

            public override void Apply(Entity @from, Owner owner)
            {
            }

            public override string ToString() => true.ToString();
        }
    }
}