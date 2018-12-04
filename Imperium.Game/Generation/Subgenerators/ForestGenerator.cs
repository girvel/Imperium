using System;
using System.Linq;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Managers;
using Imperium.Game.Common;
using Province.Vector;

namespace Imperium.Game.Generation.Subgenerators
{
    public class ForestGenerator : IAreaGenerator
    {
        public float MinimalTemperature = -7, MaximalTemperature = 30, MaximalChance = 1, MinimalHumidity = 0.1f;
        
        public void Generate(Area area, EcsManager ecs, Random random)
        {
            void Replace(Entity old, Entity prototype, Vector position)
            {
                area.Move(ecs.EntityManager.Create(prototype).GetComponent<Position>(), position);
                ecs.EntityManager.Destroy(old);
            }
            
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
                            Replace(plainPosition.Parent, Building.Forest, vector);
                        });
                }
            }
        }
    }
}