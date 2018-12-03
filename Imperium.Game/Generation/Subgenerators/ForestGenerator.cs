using System;
using System.Linq;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game.Common;

namespace Imperium.Game.Generation.Subgenerators
{
    public class ForestGenerator : IAreaGenerator
    {
        public float MinimalTemperature = -7, MaximalTemperature = 30, MaximalChance = 1, MinimalHumidity = 0.1f;
        
        public void Generate(Area area, EcsManager ecs, Random random)
        {
            foreach (var vector in area.Size.Range())
            {
                var plainPosition = area[vector].FirstOrDefault(p => p.Parent.Prototype == Building.Plain);

                if (plainPosition != null)
                {
                    random.Chance(
                        MaximalChance * (1 - 2 / (MaximalTemperature - MinimalTemperature) *
                                             Math.Abs(area.GetTemperature(vector) + MinimalTemperature)),
                        () =>
                        {
                            ecs.EntityManager.Destroy(plainPosition.Parent);
                            area.Move(ecs.EntityManager.Create(Building.Forest).GetComponent<Position>(), vector);
                        });
                }
            }
        }
    }
}