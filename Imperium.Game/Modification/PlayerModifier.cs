using System;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Game.Common;
using Imperium.Game.Prototypes;

namespace Imperium.Game.Modification
{
    public class PlayerModifier
    {
        public void Modify(Area area, Player player, Random random)
        {
            var position = random.NextPosition(area.Size);
            
            area.Slice<Building>()[position] = Building.WoodenHouse;
            area.Slice<Building>()[position].GetComponent<Owned>().Owner = player;
        }
    }
}