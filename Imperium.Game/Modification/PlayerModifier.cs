using System;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Game.Common;
using Imperium.Game.Prototypes;
using Province.Vector;

namespace Imperium.Game.Modification
{
    public class PlayerModifier
    {
        public void Modify(Area area, Player player, Random random)
        {
            Vector position;
            do
            {
                position = random.NextPosition(area.Size);
            }
            while (area.Slice<Landscape>()[position] < Landscape.Water 
                   || area.Slice<Building>()[position] < Building.Mountain);
            
            area.Slice<Building>()[position] = Building.WoodenHouse;
            area.Slice<Building>()[position].GetComponent<Owned>().Owner = player;
            
            do
            {
                position = random.NextPosition(area.Size);
            }
            while (area.Slice<Landscape>()[position] < Landscape.Water 
                   || area.Slice<Building>()[position] < Building.Mountain);

            area.Slice<Squad>()[position] = Squad.Test;
            area.Slice<Squad>()[position].GetComponent<Owned>().Owner = player;
        }
    }
}