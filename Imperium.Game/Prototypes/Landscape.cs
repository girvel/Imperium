using Imperium.Core.Systems.Landscape;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Managers;

namespace Imperium.Game.Prototypes
{
    public class Landscape : PrototypeContainer
    {
        public Entity
            Plain,
            Water;

        protected override void InitializePrototypes(EcsManager ecs)
        {
            Plain
                = new Entity("Plain")
                  | new Placer()
                  | new Terrain(TerrainType.Plain);

            Water
                = new Entity("Water")
                  | new Placer()
                  | new Terrain(TerrainType.Water);
        }
    }
}