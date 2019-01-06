using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading
{
    public class PriceUpgrade : Upgrade
    {
        public IResources Price { get; }

        public PriceUpgrade()
        {
            
        }

        public PriceUpgrade(Entity result, IResources price) : base(result)
        {
            Price = price;
        }

        public override bool IsPossible(Entity from, Player player) => player.Resources.Enough(Price);

        public override void Apply(Entity @from, Player player)
        {
            player.Resources = player.Resources.Substracted(Price);
        }
    }
}