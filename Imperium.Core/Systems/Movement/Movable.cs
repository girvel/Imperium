using System;
using Imperium.Core.Systems.Order;
using Imperium.Core.Systems.Placing;
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



        public bool Move(Vector to)
        {
            var isPossible = to.IsInside(Ecs.GetSystem<Area>().Size);

            if (isPossible)
            {
                Parent.GetComponent<Executor>().OrderQueue.Enqueue(new MovementOrder{Movable = this, To = to});
            }

            return isPossible;
        }
    }
}