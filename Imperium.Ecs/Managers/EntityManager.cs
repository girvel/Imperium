﻿using System.Collections.Generic;
using System.Linq;

namespace Imperium.Ecs.Managers
{
    public class EntityManager
    {
        public List<Entity> Entities { get; } = new List<Entity>();
        
        public EcsManager Ecs { get; set; }



        public Entity Create(Entity original = null, Entity previous = null)
        {
            var newEntity = new Entity
            {
                Ecs = Ecs,
                Original = original,
                Name = original?.Name,
            };

            if (original != null)
            {
                foreach (var c in original.Components)
                {
                    newEntity.AddComponent(
                        (Component) previous?.Components.FirstOrDefault(pc => c.GetType() == pc.GetType())?.Clone() 
                        ?? Ecs.ComponentManager.CreateClone(c));
                }
            }

            Destroy(previous);
            
            Entities.Add(newEntity);
            return newEntity;
        }



        public void Destroy(Entity target)
        {
            if (target != null)
            {
                foreach (var component in target.Components)
                {
                    Ecs.ComponentManager.Unregister(component);
                }
                
                Entities.Remove(target);
            }
        }
    }
}