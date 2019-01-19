using System.Collections.Generic;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Order
{
    public class Executor : RegisteredComponent<OrderManager, Executor>
    {
        public Queue<IOrder> OrderQueue { get; } = new Queue<IOrder>();
    }
}