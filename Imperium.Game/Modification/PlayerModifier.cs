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
        public void Modify(Area area, Owner owner, Random random)
        {
            Vector position;
            do
            {
                position = random.NextPosition(area.Size);
            }
            while (area.ContainerSlice<Landscape>()[position] < Landscape.Water 
                   || area.ContainerSlice<Building>()[position] < Building.Mountain);
            
            area.ContainerSlice<Building>()[position] = Building.WoodenHouse;
            area.ContainerSlice<Building>()[position].GetComponent<Owned>().Owner = owner;
            
            do
            {
                position = random.NextPosition(area.Size);
            }
            while (area.ContainerSlice<Landscape>()[position] < Landscape.Water 
                   || area.ContainerSlice<Building>()[position] < Building.Mountain);

            area.ContainerSlice<Squad>()[position] = Squad.Test;
            area.ContainerSlice<Squad>()[position].GetComponent<Owned>().Owner = owner;
        }
    }
}