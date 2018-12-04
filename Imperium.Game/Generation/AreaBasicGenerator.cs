using System;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game.Generation.Subgenerators;

namespace Imperium.Game.Generation
{
    public class AreaBasicGenerator : IAreaGenerator
    {
        public Pregenerator Pregenerator { get; set; } = new Pregenerator();
        
        public LandGenerator LandGenerator { get; set; } = new LandGenerator();

        public ForestGenerator ForestGenerator { get; set; } = new ForestGenerator();

        public IAreaGenerator[] Generators => new IAreaGenerator[]{ Pregenerator, LandGenerator, ForestGenerator };
        
        
        
        public void Generate(Area area, EcsManager ecs, Random random)
        {
            foreach (var generator in Generators)
            {
                generator.Generate(area, ecs, random);
            }
        }
    }
}