using Imperium.Core;
using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;

namespace Imperium.Game
{
    public class EcsFactory
    {
        public EcsManager Generate()
        {
            var ecs = EcsManager.CreateNew();
            
            var area = new Area(new Vector(5, 5));
            new AreaFactory().Generate(area, ecs);
            
            ecs.SystemManager.Register(new AreaSystem(area));
            ecs.SystemManager.Register(new PlayerSystem());

            return ecs;
        }
    }
}