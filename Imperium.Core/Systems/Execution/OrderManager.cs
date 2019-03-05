using System;
using System.Linq;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Execution
{
    public class OrderManager : ParallelRegistrationSystem<Executor>
    {
        public override void UpdateSubject(Executor executor)
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