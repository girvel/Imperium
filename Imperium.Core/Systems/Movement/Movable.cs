using System;
using Imperium.Core.Systems.Order;
using Imperium.Ecs;
using Imperium.Ecs.Attributes;
using Province.Vector;

namespace Imperium.Core.Systems.Movement
{
    [RequiresComponents(typeof(Executor))]
    public class Movable : Component
    {
        public TimeSpan MovementDelay { get; set; }

        public new Movable Prototype => (Movable) base.Prototype;



        public void Move(Vector to)
        {
            Parent.GetComponent<Executor>().OrderQueue.Enqueue(new MovementOrder{Movable = this, To = to});
        }
    }
}