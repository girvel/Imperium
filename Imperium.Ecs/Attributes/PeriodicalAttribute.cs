using System;

namespace Imperium.Ecs.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PeriodicalAttribute : Attribute
    {
        public string CounterFieldName { get; }
        
        public PeriodicalAttribute(string counterFieldName)
        {
            CounterFieldName = counterFieldName;
        }
    }
}