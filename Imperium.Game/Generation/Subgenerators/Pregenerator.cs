using System;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game.Generation.Common;

namespace Imperium.Game.Generation.Subgenerators
{
    public class Pregenerator : IAreaSubgenerator
    {
        public void Generate(AreaSlice buildingSlice, AreaSlice landscapeSlice, Random random)
        {
            foreach (var position in landscapeSlice.Size.Range())
            {
                landscapeSlice[position] = Landscape.Water;
                buildingSlice[position] = Building.None;
            }
        }
    }
}