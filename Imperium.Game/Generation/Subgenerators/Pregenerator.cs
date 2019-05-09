using System;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game.Prototypes;

namespace Imperium.Game.Generation.Subgenerators
{
    public class Pregenerator : IAreaSubgenerator
    {
        public void Generate(Area area, EcsManager ecs, Random random)
        {
            var landscape = ecs.GetContainer<Landscape>();
            var building = ecs.GetContainer<Building>();
            
            foreach (var position in area.Size.Range())
            {
                area.ContainerSlice<Landscape>()[position] = landscape.Water;
                area.ContainerSlice<Building>()[position] = building.None;
            }
        }
    }
}