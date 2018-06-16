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
                foreach (var c in original.Components.Select(c => Ecs.ComponentManager.CreateClone(c)))
                {
                    newEntity.AddComponent(c);
                }
            }
            
            Entities.Add(newEntity);
            return newEntity;
        }
    }
}