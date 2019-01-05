using System.Linq;
using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading
{
    public class Upgradable : Component
    {
        public Upgrade[] Upgrades { get; set; }



        public Upgradable(params Upgrade[] upgrades)
        {
            Upgrades = upgrades;
        }

        public bool Upgrade(Player owner, Upgrade upgrade = null)
        {
            upgrade = upgrade ?? Upgrades[0];

            var success = Upgrades.Contains(upgrade) && upgrade.TryUpgrade(Parent, owner);
            
            if (success)
            {
                var ownedComponent = Ecs.EntityManager.Create(upgrade.Result, Parent).GetComponent<Owned>();

                if (ownedComponent != null)
                {
                    ownedComponent.Owner = owner;
                }
            }

            return success;
        }
    }
}