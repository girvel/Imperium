using Imperium.Ecs;
using Province.Vector;

namespace Imperium.Core.Systems.Placing
{
    public class Position : Component
    {
        public Vector Coordinates { get; set; }


        public override void Start()
        {
            base.Start();
            
            Ecs.SystemManager.GetSystem<Area>().Move(this, Coordinates);
        }

        public override void Destroy()
        {
            base.Destroy();
            
            Ecs.SystemManager.GetSystem<Area>().Remove(this);
        }

        public override string ToString() => $"[{GetType().Name}: {Coordinates}]";
    }
}