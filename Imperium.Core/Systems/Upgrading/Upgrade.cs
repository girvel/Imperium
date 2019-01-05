using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading
{
    public abstract class Upgrade
    {
        public Entity Result { get; set; }

        protected Upgrade()
        {
        }

        protected Upgrade(Entity result)
        {
            Result = result;
        }

        public bool TryUpgrade(Entity from, Player player)
        {
            var success = IsPossible(from, player);

            if (success) Apply(from, player);

            return success;
        }

        public abstract bool IsPossible(Entity from, Player player);

        public abstract void Apply(Entity from, Player player);
    }
}