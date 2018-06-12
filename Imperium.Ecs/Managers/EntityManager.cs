using System.Collections.Generic;
using System.Linq;

namespace Imperium.Ecs.Managers
{
    public class EntityManager
    {
        public List<Entity> Entities { get; } = new List<Entity>();
        
        public EcsManager Ecs { get; set; }



        public Entity CreateNew(Entity original = null)
        {
            var newEntity = new Entity
            {
                Ecs = Ecs,
                Original = original,
                Name = original?.Name,
            };

            if (original != null)
            {
                newEntity.Components = original.Components.Select(c => Ecs.ComponentManager.CreateClone(c)).ToList();
            }
            
            Entities.Add(newEntity);
            return newEntity;
        }
    }
}