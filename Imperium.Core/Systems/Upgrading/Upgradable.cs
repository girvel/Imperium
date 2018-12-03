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
            
            if (Upgrades.Contains(upgrade) && owner.Resources.Enough(upgrade.Price))
            {
                owner.Resources = owner.Resources.Substracted(upgrade.Price);
                var ownedComponent = Ecs.EntityManager.Create(upgrade.Result, Parent).GetComponent<Owned>();

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