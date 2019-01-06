using System;
using System.Collections.Generic;
using System.Linq;
using Imperium.Core.Systems.Placing;
using Province.Vector;

namespace Imperium.Core.Systems.Movement
{
    public class MovementManager : Ecs.RegistrationSystem<Movable>
    {
        public List<MovementTask> CurrentTasks { get; set; } = new List<MovementTask>();



        public void AddTask(MovementTask task)
        {
            CurrentTasks.Add(task);
        }

        public override void Update()
        {
            base.Update();

            foreach (var task in CurrentTasks)
            {
                task.Movable.MovementDelay -= Ecs.UpdateDelay;

                var newPosition = task.Placer.Position;
                
                while (task.Movable.MovementDelay <= TimeSpan.Zero)
                {
                    if (newPosition == task.To)
                    {
                        task.Movable.MovementDelay = task.Movable.Prototype.MovementDelay;
                        break;
                    }
                    
                    task.Movable.MovementDelay += task.Movable.Prototype.MovementDelay;

                    var direction = task.To - task.Placer.Position;
                    
                    newPosition
                        += direction.X != 0
                            ? new Vector(1, 0) * Math.Sign(direction.X)
                            : new Vector(0, 1) * Math.Sign(direction.Y);
                }
                
                Ecs.SystemManager.GetSystem<Area>().Move(task.Placer, newPosition);
            }

            CurrentTasks = CurrentTasks.Where(t => t.To != t.Placer.Position).ToList();
        }
    }
}