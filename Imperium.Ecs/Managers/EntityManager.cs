﻿using System;
using System.Collections.Generic;
using System.Linq;
using Province.ToString;

namespace Imperium.Ecs.Managers
{
    public class EntityManager
    {
        public List<Entity> Entities { get; } = new List<Entity>();
        
        public EcsManager Ecs { get; set; }

        public event Action<Entity> OnEntityCreate, OnEntityDestroy;



        public Entity Create(Entity prototype = null, Entity previous = null)
        {
            var newEntity = new Entity
            {
                Ecs = Ecs,
                Prototype = prototype,
                Name = prototype?.Name,
            };

            if (prototype != null)
            {
                foreach (var c in prototype.Components)
                {
                    var previousComponent = previous?.Components.FirstOrDefault(pc => c.GetType() == pc.GetType());
                    newEntity.AddComponent((previousComponent ?? c).Clone());
                }
            }

            Destroy(previous);
            
            Entities.Add(newEntity);
            OnEntityCreate?.Invoke(newEntity);

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
                OnEntityDestroy?.Invoke(target);
            }
        }
    }
}