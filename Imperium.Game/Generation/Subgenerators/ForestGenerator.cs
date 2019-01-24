﻿using System;
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
        
        public void Generate(Area area, Random random)
        {
            foreach (var vector in area.Size.Range())
            {
                if (area.ContainerSlice<Landscape>()[vector] < Landscape.Plain)
                {
                    random.Chance(
                        MaximalChance * (1 - 2 / (MaximalTemperature - MinimalTemperature) *
                                         Math.Abs(area.GetTemperature(vector) + MinimalTemperature)),
                        () =>
                        {
                            area.ContainerSlice<Building>()[vector] = Building.Forest;
                        });
                }
            }
        }
    }
}