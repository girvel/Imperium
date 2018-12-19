using System;
using System.Linq;
using System.Reflection;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Managers;
using Imperium.Game.Generation.Common;
using Imperium.Game.Generation.Subgenerators;

namespace Imperium.Game.Generation
{
    public class AreaBasicGenerator
    {
        public Pregenerator Pregenerator { get; set; } = new Pregenerator();
        
        public LandGenerator LandGenerator { get; set; } = new LandGenerator();
        
        public MountainsGenerator MountainsGenerator { get; set; } = new MountainsGenerator();

        public ForestGenerator ForestGenerator { get; set; } = new ForestGenerator();
        
        

        public IAreaSubgenerator[] Subgenerators => new IAreaSubgenerator[]
        {
            Pregenerator, 
            LandGenerator, 
            MountainsGenerator,
            ForestGenerator, 
        };
        
        
        
        public void Generate(Area area, EcsManager ecs, Random random)
        {
            foreach (var generator in Subgenerators)
            {
                generator.Generate(area & typeof(Building), area & typeof(Landscape), random);
            }
        }
    }
}