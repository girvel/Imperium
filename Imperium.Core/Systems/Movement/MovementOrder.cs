using System;
using Imperium.Core.Systems.Execution;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Province.ExtendedFramework;
using Province.Vector;

namespace Imperium.Core.Systems.Movement
{
    public class MovementOrder : Order
    {
        public Movable Movable => Subject.GetComponent<Movable>();

        public Placer Placer => Subject.GetComponent<Placer>();
        
        public Vector To { get; set; }
        
        
        
        public MovementOrder(Vector to)
        {
            To = to;
        }

        public override TimeSpan Update(TimeSpan maxTime)
        {
            var steps = maxTime.Divided(Movable.Prototype.MovementDelay);
            
            var newPosition = Placer.Position;
            Vector delta;
            
            Vector GetMovement() => new Vector(
                (int) Math.Round(delta.X / delta.Magnitude), 
                (int) Math.Round(delta.Y / delta.Magnitude));

            Movable.MovementDelay -= Movable.Prototype.MovementDelay.Multiplied(steps % 1);

            if (Movable.MovementDelay < TimeSpan.Zero)
            {
                Movable.MovementDelay += Movable.Prototype.MovementDelay;
                steps++;
            }
            
            while (steps >= 1 && To != newPosition)
            {
                delta = To - newPosition;

                newPosition += GetMovement();

                steps--;
            }
            
            if (newPosition != Placer.Position) Placer.Move(newPosition);

            return To == newPosition
                ? maxTime - Movable.Prototype.MovementDelay.Multiplied(steps) - Movable.MovementDelay
                : maxTime;
        }
    }
}