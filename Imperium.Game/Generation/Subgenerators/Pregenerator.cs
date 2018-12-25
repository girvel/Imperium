using System;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game.Prototypes;

namespace Imperium.Game.Generation.Subgenerators
{
    public class Pregenerator : IAreaSubgenerator
    {
        public void Generate(Area area, Random random)
        {
            foreach (var position in area.Size.Range())
            {
                (area & typeof(Landscape))[position] = Landscape.Water;
                (area & typeof(Building))[position] = Building.None;
            }
        }
    }
}