using System;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game.Common;
using Imperium.Game.Prototypes;
using Province.Vector;

namespace Imperium.Game.Modification
{
    public class PlayerModifier
    {
        public void Modify(Area area, EcsManager ecs, Owner owner, Random random)
        {
            var landscape = ecs.GetContainer<Landscape>();
            var building = ecs.GetContainer<Building>();
            var squad = ecs.GetContainer<Squad>();
            
            Vector position;
            do
            {
                position = random.NextPosition(area.Size);
            }
            while (area.ContainerSlice<Landscape>()[position] < landscape.Water 
                   || area.ContainerSlice<Building>()[position] < building.Mountain);
            
            area.ContainerSlice<Building>()[position] = building.WoodenHouse;
            area.ContainerSlice<Building>()[position].GetComponent<Owned>().Owner = owner;
            
            do
            {
                position = random.NextPosition(area.Size);
            }
            while (area.ContainerSlice<Landscape>()[position] < landscape.Water 
                   || area.ContainerSlice<Building>()[position] < building.Mountain);

            area.ContainerSlice<Squad>()[position] = squad.Test;
            area.ContainerSlice<Squad>()[position].GetComponent<Owned>().Owner = owner;
        }
    }
}