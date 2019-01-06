using System;
using Imperium.Ecs;
using Province.Vector;

namespace Imperium.Core.Systems.Movement
{
    public class Movable : RegisteredComponent<MovementManager, Movable>
    {
        public TimeSpan MovementDelay { get; set; }

        public new Movable Prototype => (Movable) base.Prototype;



        public void Move(Vector to)
        {
            System.AddTask(new MovementTask{Movable = this, To = to,});
        }
    }
}