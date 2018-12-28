using System;
using System.Linq;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Managers;
using Imperium.Game.Common;
using Imperium.Game.Prototypes;
using Province.Vector;
// ReSharper disable LoopVariableIsNeverChangedInsideLoop

namespace Imperium.Game.Generation.Subgenerators
{
    public class LandGenerator : IAreaSubgenerator
    {
        public float LandFraction = 0.4f, SeasideBorder = 0.6f;
        
        public void Generate(Area area, Random random)
        {
            var landscapeSlice = area.Slice<Landscape>();
            
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
                                = Vector.Clamp(
                                    vector + sourcePosition, 
                                    Vector.Zero,
                                    area.Size - Vector.One);

                            if (landscapeSlice[newPosition] < Landscape.Water)
                            {
                                counter++;
                                landscapeSlice[newPosition] = Landscape.Plain;
                            }
                        });
                }
            }
        }
    }
}