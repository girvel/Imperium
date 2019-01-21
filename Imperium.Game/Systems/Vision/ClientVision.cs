using Imperium.Application.Common;
using Imperium.CommonData;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Core.Systems.Vision;

namespace Imperium.Game.Systems.Vision
{
    public class ClientVision : AbstractVision
    {
        public override VisionDto GetCurrentVision(Player player)
        {
            var area = Ecs.SystemManager.GetSystem<Area>();
            var result = new PlaceDto[area.Size.X, area.Size.Y];
            var visibility = GetVisibility(player);
            
            foreach (var v in area.Size.Range())
            {
                result[v.X, v.Y] = visibility[v.X, v.Y] ? area.GetPlaceDto(v) : null;
            }

            return new VisionDto
            {
                Visibility = visibility,
                Grid = result,
            };
        }
    }
}