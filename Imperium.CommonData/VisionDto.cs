using System.Linq;

namespace Imperium.CommonData
{
    public class VisionDto
    {
        public bool[,] Visibility;
        
        public PlaceDto[,] Grid;

        public override string ToString() =>
            $"[{GetType().Name} | Visibles: {Grid.Cast<PlaceDto>().Count(d => d != null)}]";
    }
}