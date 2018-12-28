using Imperium.CommonData;
using Imperium.Core.Systems.Placing;
using Imperium.Game.Prototypes;
using Province.Vector;

namespace Imperium.Application.Common
{
    public static class DtoHelper
    {
        public static PlaceDto GetPlaceDto(this Area area, Vector position)
        {
            return new PlaceDto
            {
                BuildingName = area.Slice<Building>()[position].Name,
                TerrainName = area.Slice<Landscape>()[position].Name,
                SquadName = area.Slice<Squad>()[position].Name,
                Temperature = area.GetTemperature(position),
            };
        }
    }
}