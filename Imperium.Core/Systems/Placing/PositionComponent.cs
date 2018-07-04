using Imperium.CommonData;
using Imperium.Ecs;
using Province.Vector;

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