using System;
using Imperium.Core.Systems.Income;
using Imperium.Core.Systems.Movement;
using Imperium.Core.Systems.Order;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Vision;
using Imperium.Ecs.Managers;
using Imperium.Game.Generation;
using Imperium.Game.Systems.Vision;
using Province.Vector;

namespace Imperium.Game
{
    public class EcsFactory
    {
        public EcsManager Generate()
        {
            var ecs = EcsManager.CreateNew();
            
            var area = new Area(new Vector(40, 40));

            var systems = new Ecs.System[]
            {
                area,
                new Ownership(Resources.Zero),
                new IncomeSystem(),
                new ClientVision(), 
                new OrderManager(), 
            };
            
            foreach (var system in systems)
            {
                ecs.SystemManager.Register(system);
            }
            
            new AreaBasicGenerator().Generate(area, ecs, new Random());

            return ecs;
        }
    }
}