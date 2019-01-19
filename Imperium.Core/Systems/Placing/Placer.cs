using Imperium.Ecs;
using Province.Vector;

namespace Imperium.Core.Systems.Placing
{
    public class Placer : Component
    {
        public Vector Position { get; set; }



        public void Move(Vector newPosition)
        {
            Ecs.GetSystem<Area>().Move(this, newPosition);
        }
        
        public override void Start()
        {
            base.Start();
            
            Ecs.SystemManager.GetSystem<Area>().Move(this, Position);
        }

        public override void Destroy()
        {
            base.Destroy();
            
            Ecs.SystemManager.GetSystem<Area>().Remove(this);
        }

        public override string ToString() => $"[{GetType().Name}: {Position}]";
    }
}