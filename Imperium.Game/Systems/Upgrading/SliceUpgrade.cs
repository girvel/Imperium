using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Upgrading;
using Imperium.Ecs;

namespace Imperium.Game.Systems.Upgrading
{
    public class SliceUpgrade<TSlice> : PriceUpgrade
    {
        public Entity RequiredEntity { get; }
        
        public SliceUpgrade()
            {}
        
        public SliceUpgrade(Entity result, IResources price, Entity requiredEntity) : base(result, price)
        {
            RequiredEntity = requiredEntity;
        }

        public override bool IsPossible(Entity from, Player player)
        {
            var area = from.Ecs.SystemManager.GetSystem<Area>();
            var position = from.GetComponent<Placer>().Position;
            
            return base.IsPossible(from, player) && area.Slice<TSlice>()[position] < RequiredEntity;
        }
    }
}