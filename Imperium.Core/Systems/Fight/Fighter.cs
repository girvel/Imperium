using Imperium.Core.Systems.Execution;
using Imperium.Ecs;
using Imperium.Ecs.Attributes;

namespace Imperium.Core.Systems.Fight
{
    [RequiresComponents(typeof(Executor))]
    public class Fighter : Component
    {
        public double Dps { get; }
        
        public Fighter(double dps)
        {
            Dps = dps;
        }

        public bool Attack(Destructible target)
        {
            var success = target != null && AttackCondition(target);
            if (success)
            {
                Parent.GetComponent<Executor>().AddOrder(new AttackOrder(target));
            }
            return success;
        }

        public virtual bool AttackCondition(Destructible target) => true;
    }
}