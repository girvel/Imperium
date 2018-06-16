using Imperium.CommonData;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Placing
{
    public class PositionComponent : Component
    {
        public Vector Position { get; set; }


        public override void Start()
        {
            Ecs.SystemManager.GetSystem<AreaSystem>().Move(this, Position);
        }
    }
}