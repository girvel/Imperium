using System;
using System.Linq;
using Imperium.Core.Systems.Placing;
using Imperium.Game.Common;
using Imperium.Game.Generation.Common;
using Province.Vector;

namespace Imperium.Game.Generation.Subgenerators
{
    public class MountainsGenerator : IAreaSubgenerator
    {
        public int Width = 5;

        public int MaximalLength = 20;

        public int NumberOfRidges = 10;
        
        
        
        public void Generate(AreaSlice buildingSlice, AreaSlice landscapeSlice, Random random)
        {
            var ridges
                = Enumerable
                    .Range(0, NumberOfRidges)
                    .Select(_ => random.NextPosition(landscapeSlice.Size))
                    .Select(p => 
                        new[]
                        {
                            p, 
                            random.NextPosition(
                                Vector.PartialMax(Vector.Zero, p - MaximalLength * Vector.One), 
                                Vector.PartialMin(landscapeSlice.Size - Vector.One, p + MaximalLength * Vector.One))
                        })
                    .ToArray();

            foreach (var ridge in ridges)
            {
                GenerateRidge(ridge[0], ridge[1], landscapeSlice, random);
            }
        }

        protected virtual void GenerateRidge(Vector from, Vector to, AreaSlice landscapeSlice, Random random)
        {
            var delta = to - from;
            var generationRadius = Mathf.Max(Width, delta.X, delta.Y) * Vector.One;
                
            foreach (
                var position 
                in Vector.Range(
                    Vector.Max(Vector.Zero, from - generationRadius), 
                    Vector.Min(landscapeSlice.Size - Vector.One, from + generationRadius)))
            {
                var internalPosition = delta.TransitionMatrix() * (position - from).ToMatrix();
                    
                var internalX = internalPosition[0, 0];
                var internalY = internalPosition[1, 0] * delta.Magnitude / Width;

                if (Math.Abs(2 * internalX - 1) <= 1 && Math.Abs(internalY) <= 1)
                {
                    random.Chance(
                        1 - Math.Abs(internalY) * 2 / (1 - Math.Abs(2 * internalX - 1)),
                        () =>
                        {
                            landscapeSlice[position] 
                                = landscapeSlice[position] < Landscape.Water 
                                    ? Landscape.Plain 
                                    : Landscape.Mountain;
                        });
                }
            }
        }
    }
}