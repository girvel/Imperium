using Imperium.Core;
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
            
            ecs.SystemManager.Register(area);
            ecs.SystemManager.Register(new PlayerSystem());
            
            new AreaFactory().Generate(area, ecs);

            return ecs;
        }
    }
}