using Imperium.Ecs;
using Province.Vector;

namespace Imperium.Core.Systems.Placing
{
    public class PositionComponent : Component
    {
        public Vector Position { get; set; }


        public override void Start()
        {
            Ecs.SystemManager.GetSystem<Area>().Move(this, Position);
        }

        public override string ToString() => $"[{GetType().Name}: {Position}]";
    }
}