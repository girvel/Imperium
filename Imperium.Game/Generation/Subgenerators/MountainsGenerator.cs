using System;
using System.Linq;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game.Common;
using Imperium.Game.Prototypes;
using Province.Vector;

namespace Imperium.Game.Generation.Subgenerators
{
    public class MountainsGenerator : IAreaSubgenerator
    {
        public int MinimalWidth = 2, MaximalWidth = 5;

        public int MaximalLength = 20;

        public int NumberOfRidges = 10;
        
        
        
        public void Generate(Area area, EcsManager ecs, Random random)
        {
            var ridges 
                = Enumerable
                    .Range(0, NumberOfRidges)
                    .Select(_ => random.NextPosition(area.Size))
                    .Select(p => 
                        new Ridge
                        {
                            From = p, 
                            To = random.NextPosition(
                                p - MaximalLength / 2 * Vector.One, 
                                p + MaximalLength / 2 * Vector.One),
                            Width = random.Next(MinimalWidth, MaximalWidth),
                        })
                    .ToArray();

            foreach (var ridge in ridges)
            {
                GenerateRidge(ridge, area, ecs, random);
            }
        }

        protected virtual void GenerateRidge(Ridge ridge, Area area, EcsManager ecs, Random random)
        {
            var landscape = ecs.GetContainer<Landscape>();
            var building = ecs.GetContainer<Building>();
            
            var delta = ridge.To - ridge.From;

            for (var px = Math.Max(0, Math.Min(ridge.From.X, ridge.To.X) - ridge.Width);
                px < Math.Min(area.Size.X - 1, Math.Max(ridge.From.X, ridge.To.X) + ridge.Width);
                px++)
            for (var py = Math.Max(0, Math.Min(ridge.From.Y, ridge.To.Y) - ridge.Width);
                py < Math.Min(area.Size.Y - 1, Math.Max(ridge.From.Y, ridge.To.Y) + ridge.Width);
                py++)
            {
                var position = new Vector(px, py);
                
                var internalPosition = delta.TransitionMatrix() * (2 * position - ridge.From - ridge.To).ToMatrix() / 2;
                    
                var x = internalPosition[0, 0];
                var y = internalPosition[1, 0] * delta.Magnitude / ridge.Width * 2;

                if (random.Chance(
                    2 * (1 - Math.Sqrt(x * x + y * y))))
                {
                    area.ContainerSlice<Landscape>()[position] = landscape.Plain;
                    area.ContainerSlice<Building>()[position] = building.Mountain;
                }
            }
        }



        protected class Ridge
        {
            public Vector From, To;

            public int Width;
        }
    }
}