using System;
using System.Security.Cryptography;
using Imperium.Core.Systems.Execution;
using Imperium.Core.Systems.Fight;
using Imperium.Core.Systems.Movement;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Vision;
using Imperium.Ecs;

namespace Imperium.Game.Prototypes
{
    public class Squad
    {
        public static readonly Entity
            Test
                = new Entity("Test")
                  | new Placer()
                  | new Owned()
                  | new Observer {VisionRange = 3}
                  | new Executor()
                  | new Movable {MovementDelay = TimeSpan.FromSeconds(3)}
                  | new Fighter(10)
                  | new Destructible(10);
    }
}