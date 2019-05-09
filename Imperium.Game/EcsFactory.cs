using System;
using Imperium.Core.Systems.Execution;
using Imperium.Core.Systems.Income;
using Imperium.Core.Systems.Movement;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Production;
using Imperium.Core.Systems.Science;
using Imperium.Core.Systems.Vision;
using Imperium.Ecs;
using Imperium.Ecs.Managers;
using Imperium.Game.Generation;
using Imperium.Game.Prototypes;
using Imperium.Game.Systems.Vision;
using Province.Vector;

namespace Imperium.Game
{
    public class EcsFactory
    {
        public EcsManager Generate()
        {
            var ecs = EcsManager.CreateNew();
            
            GeneratePrototypes(ecs);
            GenerateSystems(ecs);

            return ecs;
        }

        private static void GeneratePrototypes(EcsManager ecs)
        {
            var containers = new PrototypeContainer[]
            {
                new Squad(),
                new Global(),
                new Science(),
                new Landscape(),
                new Building(), 
            };

            foreach (var prototypeContainer in containers)
            {
                ecs.PrototypeManager.Register(prototypeContainer);
            }
        }

        private static void GenerateSystems(EcsManager ecs)
        {
            var area = new Area(new Vector(40, 40));

            var systems = new Ecs.System[]
            {
                area,
                new Ownership(Resources.Zero),
                new IncomeSystem(),
                new ClientVision(), 
                new OrderManager(), 
                new ResearchSystem(ecs.GetContainer<Science>().Test), 
                new ProductionManager(), 
            };
            
            foreach (var system in systems)
            {
                ecs.SystemManager.Register(system);
            }
            
            new AreaBasicGenerator().Generate(area, ecs, new Random());
        }
    }
}