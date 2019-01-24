using System;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Execution
{
    public abstract class Order
    {
        public Entity Subject { get; internal set; }
        
        public abstract TimeSpan Update(TimeSpan maxTime);
    }
}