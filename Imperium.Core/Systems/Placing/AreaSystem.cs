using Imperium.Core.Common;

namespace Imperium.Core.Systems.Placing
{
    public class AreaSystem : Ecs.System
    {
        public Area Area { get; set; }

        

        public AreaSystem(Area area)
        {
            Area = area;
        }
        
        public void Move(PositionComponent component, Vector newPosition)
        {
            Area[component.Position].Remove(component);
            component.Position = newPosition;
            Area[component.Position].Add(component);
        }
    }
}