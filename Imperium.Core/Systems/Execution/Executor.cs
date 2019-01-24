using System.Collections.Generic;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Execution
{
    public class Executor : RegisteredComponent<OrderManager, Executor>
    {
        public Queue<Order> OrderQueue { get; } = new Queue<Order>();



        public void AddOrder(Order order)
        {
            order.Subject = Parent;
            OrderQueue.Enqueue(order);
        }
    }
}