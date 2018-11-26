using System.Linq;
using Imperium.Core.Systems.Owning;
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

        public bool Upgrade(Player owner = null, Entity upgrade = null)
        {
            upgrade = upgrade ?? Upgrades[0];
            
            if (Upgrades.Contains(upgrade))
            {
                var ownedComponent = Ecs.EntityManager.Create(upgrade, Parent).GetComponent<OwnedComponent>();

                if (ownedComponent != null)
                {
                    ownedComponent.Owner = owner;
                }
                
                return true;
            }

            return false;
        }
    }
}