using System.Linq;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading
{
    public class UpgradableComponent : Component
    {
        public Entity[] Upgrades { get; set; }



        public UpgradableComponent(params Entity[] upgrades)
        {
            Upgrades = upgrades;
        }

        public bool Upgrade(Entity upgrade = null)
        {
            upgrade = upgrade ?? Upgrades[0];
            
            if (Upgrades.Contains(upgrade))
            {
                Ecs.EntityManager.Create(upgrade, Parent);
                return true;
            }

            return false;
        }
    }
}