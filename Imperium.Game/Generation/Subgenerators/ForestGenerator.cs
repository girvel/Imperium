using System;
using System.Linq;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Managers;
using Imperium.Game.Common;
using Imperium.Game.Prototypes;
using Province.Vector;

namespace Imperium.Game.Generation.Subgenerators
{
    public class ForestGenerator : IAreaSubgenerator
    {
        public float MinimalTemperature = -7, MaximalTemperature = 30, MaximalChance = 1, MinimalHumidity = 0.1f;
        
        public void Generate(AreaSlice buildingSlice, AreaSlice landscapeSlice, Random random)
        {
            foreach (var vector in buildingSlice.Area.Size.Range())
            {
                if (!(landscapeSlice[vector] < Landscape.Water))
                {
                    random.Chance(
                        MaximalChance * (1 - 2 / (MaximalTemperature - MinimalTemperature) *
                                         Math.Abs(buildingSlice.Area.GetTemperature(vector) + MinimalTemperature)),
                        () =>
                        {
                            buildingSlice[vector] = Building.Forest;
                        });
                }
            }
        }
    }
}