using System.Collections.Generic;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Owning
{
    public class PlayerSystem : Ecs.System
    {
        public List<Player> Players { get; set; } = new List<Player>();
    }
}