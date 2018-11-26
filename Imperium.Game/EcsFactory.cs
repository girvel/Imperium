using Imperium.Core.Systems.Income;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Province.Vector;

namespace Imperium.Game
{
    public class EcsFactory
    {
        public EcsManager Generate()
        {
            var ecs = EcsManager.CreateNew();
            
            var area = new Area(new Vector(5, 5));

            var systems = new Ecs.System[]
            {
                area,
                new PlayerSystem(Resources.Zero),
                new IncomeSystem(),
            };
            
            foreach (var system in systems)
            {
                ecs.SystemManager.Register(system);
            }
            
            new AreaFactory().Generate(area, ecs);

            return ecs;
        }
    }
}