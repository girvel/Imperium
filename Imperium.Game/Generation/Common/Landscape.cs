using Imperium.Core.Systems.Landscape;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;

namespace Imperium.Game.Generation.Common
{
    public static class Landscape
    {
        public static Entity
            Plain = new Entity("Plain")
                | new Placer()
                | new Terrain(TerrainType.Plain),
            
            Water = new Entity("Water")
                | new Placer()
                | new Terrain(TerrainType.Water),
            
            Mountain = new Entity("Mountain")
                | new Placer()
                | new Terrain(TerrainType.Mountains);
    }
}