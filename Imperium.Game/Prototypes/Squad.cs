using Imperium.Core.Systems.Placing;
using Imperium.Ecs;

namespace Imperium.Game.Prototypes
{
    public class Squad
    {
        public static readonly Entity
            Test
                = new Entity("Test")
                  | new Placer();
    }
}