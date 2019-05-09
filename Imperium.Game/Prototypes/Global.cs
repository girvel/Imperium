using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Science;
using Imperium.Ecs;
using Imperium.Ecs.Managers;

namespace Imperium.Game.Prototypes
{
    public class Global : PrototypeContainer
    {
        public Entity
            Player;

        protected override void InitializePrototypes(EcsManager ecs)
        {
            Player
                = new Entity("<player>")
                  | new Owner()
                  | new ResearchHolder();
        }
    }
}