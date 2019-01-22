using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Game.Prototypes
{
    public class Global
    {
        private Global()
        {
        }

        public static Entity
            Player
                = new Entity("<player>")
                  | new Owner();
    }
}