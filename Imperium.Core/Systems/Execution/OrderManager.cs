using System;
using System.Linq;

namespace Imperium.Core.Systems.Execution
{
    public class OrderManager : Ecs.RegistrationSystem<Executor>
    {
        public override void Update()
        {
            base.Update();

            foreach (var executor in Subjects)
            {
                var time = Ecs.UpdateDelay;

                while (executor.OrderQueue.Any())
                {
                    var result = executor.OrderQueue.Peek().Update(time);
                    time -= result;

                    if (time <= TimeSpan.Zero) break;
                    
                    executor.OrderQueue.Dequeue();
                }
            }
        }
    }
}