﻿using System;
using System.Collections.Generic;
using System.Linq;
using Imperium.Ecs.Managers;

namespace Imperium.Ecs
{
    public class Entity
    {
        public string Name { get; set; }

        public List<Component> Components { get; } = new List<Component>();
        
        public EcsManager Ecs { get; set; }



        public void AddComponent(Component component)
        {
            component.Ecs = Ecs;
            component.Owner = this;
            
            Components.Add(component);
            Ecs.ComponentManager.Register(component);
            
            component.Start();
        }
        
        public T GetComponent<T>()
        {
            return Components.OfType<T>().First();
        }
    }
}