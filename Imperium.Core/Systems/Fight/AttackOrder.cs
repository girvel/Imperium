using System;
using Imperium.Core.Common;
using Imperium.Core.Systems.Execution;

namespace Imperium.Core.Systems.Fight
{
    public class AttackOrder : Order
    {
        public Destructible Target { get; }
        
        public AttackOrder(Destructible target)
        {
            Target = target;
        }

        public override TimeSpan Update(TimeSpan maxTime)
        {
            var seconds = Math.Min(Target.HealthPoints / Subject.GetComponent<Fighter>().Dps, maxTime.TotalSeconds);

            Target.Damage((float) (seconds * Subject.GetComponent<Fighter>().Dps));
            return TimeSpan.FromSeconds(seconds);
        }
    }
}