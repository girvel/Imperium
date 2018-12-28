using Imperium.Core.Systems.Landscape;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;

namespace Imperium.Game.Prototypes
{
    public class Landscape
    {
        public static Entity
            Plain
                = new Entity("Plain")
                  | new Placer()
                  | new Terrain(TerrainType.Plain),

            Water
                = new Entity("Water")
                  | new Placer()
                  | new Terrain(TerrainType.Water);
    }
}