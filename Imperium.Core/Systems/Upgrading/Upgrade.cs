using Imperium.Core.Common;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading
{
    public class Upgrade
    {
        public Entity Result { get; set; }
        
        public IResources Price { get; set; }

        public Upgrade(Entity result, IResources price)
        {
            Result = result;
            Price = price;
        }
    }
}