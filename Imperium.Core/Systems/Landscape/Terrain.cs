using Imperium.Ecs;

namespace Imperium.Core.Systems.Landscape
{
    public class Terrain : Component
    {
        public TerrainType TerrainType { get; set; }
        
        
        
        public Terrain(TerrainType terrainType)
        {
            TerrainType = terrainType;
        }
    }
}