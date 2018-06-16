using Imperium.Core;
using Imperium.Ecs.Managers;

namespace Imperium.Game
{
    public class GameData
    {
        public EcsManager Ecs { get; set; }
        
        public Area Area { get; set; }
    }
}