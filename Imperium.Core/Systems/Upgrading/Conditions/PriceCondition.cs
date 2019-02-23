using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading.Conditions
{
    public class PriceCondition : Condition
    {
        public InternalResources Price { get; }

        

        public PriceCondition(InternalResources price)
        {
            Price = price;
        }

        

        public override bool IsPossible(Entity @from, Owner owner)
        {
            return owner.Resources.Enough(Price);
        }

        public override void Apply(Entity @from, Owner owner)
        {
            owner.Resources -= Price;
        }
    }
}