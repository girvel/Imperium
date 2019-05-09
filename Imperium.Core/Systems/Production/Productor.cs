using System;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Attributes;

namespace Imperium.Core.Systems.Production
{
    public class Productor : RegisteredComponent<Productor>
    {
        public TimeSpan ProductionTime { get; set; }
        
        public Entity ResultPrototype { get; set; }



        public Productor(TimeSpan productionTime, Entity resultPrototype)
        {
            ProductionTime = productionTime;
            ResultPrototype = resultPrototype;
        }
        
        [Periodical("ProductionTime")]
        public void ProductEntity()
        {
            var newEntity = Ecs.EntityManager.Create(ResultPrototype);

            var newPlacer = newEntity.GetComponent<Placer>();
            var productorPlacer = Parent.GetComponent<Placer>();
            if (newPlacer != null && productorPlacer != null)
            {
                newPlacer.Move(productorPlacer.Position);
            }
        }
    }
}