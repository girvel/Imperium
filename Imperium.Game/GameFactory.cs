using Imperium.CommonData;
using Imperium.Core;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Province.Vector;

namespace Imperium.Game
{
    public class GameFactory
    {
        public GameData Generate()
        {
            var ecs = EcsManager.CreateNew();
            
            var area = new Area(new Vector(5, 5));
            
            ecs.SystemManager.Register(new AreaSystem(area));
            ecs.SystemManager.Register(new PlayerSystem());
            
            new AreaFactory().Generate(area, ecs);
            
            return new GameData
            {
                Ecs = ecs,
                Area = area,
            };
        }
    }
}