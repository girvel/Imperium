using System;
using System.Linq;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Managers;
using Imperium.Game.Common;
using Province.Vector;
// ReSharper disable LoopVariableIsNeverChangedInsideLoop

namespace Imperium.Game.Generation.Subgenerators
{
    public class LandGenerator : IAreaGenerator
    {
        public float LandFraction = 0.4f, SeasideBorder = 0.6f;
        
        public void Generate(Area area, EcsManager ecs, Random random)
        {
            void Replace(Entity old, Entity prototype, Vector position)
            {
                area.Move(ecs.EntityManager.Create(prototype).GetComponent<Position>(), position);
                ecs.EntityManager.Destroy(old);
            }
            
            var maximalLandNumber = LandFraction * area.Size.X * area.Size.Y;
            var counter = 0;

            while (counter < maximalLandNumber - 1)
            {
                var radius = random.NextDouble() * Math.Sqrt((maximalLandNumber - counter) / Math.PI) / 2;
                var sourcePosition = random.NextPosition(area.Size);

                var sizeVector = (int) radius * Vector.One;
                foreach (var vector in (sizeVector * 2 + Vector.One).Range().Select(v => v - sizeVector))
                {
                    var part = (vector.Magnitude - SeasideBorder * radius) / (radius * (1 - SeasideBorder));
                    random.Chance(
                        0.5 * (Math.Abs(part - 1) - Math.Abs(part) + 1),
                        () =>
                        {
                            var newPosition 
                                = Vector.Clamp(vector + sourcePosition, Vector.Zero, area.Size - Vector.One);

                            if (area[newPosition].All(p => p.Parent.Prototype != Building.Plain))
                            {
                                counter++;
                                Replace(area[newPosition].Select(p => p.Parent).First(p => p.Prototype == Building.Water), Building.Plain, newPosition);
                            }
                        });
                }
            }
        }
    }
}