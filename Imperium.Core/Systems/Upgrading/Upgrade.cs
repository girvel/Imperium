using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading
{
    public class Upgrade
    {
        public Entity Result { get; set; }
        public Condition Condition { get; }


        public Upgrade(Entity result, Condition condition)
        {
            Result = result;
            Condition = condition;
        }

        public bool TryUpgrade(Entity from, Owner owner)
        {
            var success = Condition.IsPossible(from, owner);

            if (success) Condition.Apply(from, owner);

            return success;
        }
    }
}