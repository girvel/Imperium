using System;
using System.Collections.Generic;
using System.Linq;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Execution
{
    public class Executor : RegisteredComponent<Executor>
    {
        public Queue<Order> OrderQueue { get; } = new Queue<Order>();


        public override void Update()
        {
            base.Update();
            
            var time = Ecs.UpdateDelay;

            while (OrderQueue.Any())
            {
                var result = OrderQueue.Peek().Update(time);
                time -= result;

                if (time <= TimeSpan.Zero) break;
                
                OrderQueue.Dequeue();
            }
        }

        public void AddOrder(Order order)
        {
            order.Subject = Parent;
            OrderQueue.Enqueue(order);
        }
    }
}