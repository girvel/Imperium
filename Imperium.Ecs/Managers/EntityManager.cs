using System.Collections.Generic;

namespace Imperium.Ecs.Managers
{
    public class EntityManager
    {
        public List<Entity> Entities { get; } = new List<Entity>();
        
        public EcsManager Ecs { get; set; }



        public Entity CreateNew()
        {
            var newEntity = new Entity
            {
                Ecs = Ecs,
            };
            
            Entities.Add(newEntity);
            return newEntity;
        }
    }
}