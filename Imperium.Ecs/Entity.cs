using System.Collections.Generic;
using System.Linq;
using Imperium.Ecs.Managers;

namespace Imperium.Ecs
{
    public class Entity
    {
        public string Name { get; set; }

        public List<Component> Components { get; set; } = new List<Component>();



        public EcsManager Ecs { get; set; }
        
        public Entity Prototype { get; set; }



        public Entity()
        {
        }
        
        public Entity(string name)
        {
            Name = name;
        }
        
        

        public void AddComponent(Component component)
        {
            component.Ecs = Ecs;
            component.Parent = this;
            
            Components.Add(component);
            Ecs.ComponentManager.Register(component);
            
            component.Start();
        }
        
        public void RemoveComponent(Component component)
        {
            Components.Remove(component);
            Ecs.ComponentManager.Unregister(component);
            
            component.Ecs = null;
            component.Parent = null;
        }
        
        public T GetComponent<T>()
        {
            return Components.OfType<T>().FirstOrDefault();
        }
        
        public override string ToString()
            => $"[Entity \"{Name}\": " +
               $"{{{Components.Aggregate("", (sum, c) => sum + ", " + c.ToString()).Substring(2)}}}]";



        /// <summary>
        /// Adds component to a prototype 
        /// </summary>
        /// <param name="e">Prototype</param>
        /// <param name="component">New component</param>
        /// <returns>Given protype</returns>
        public static Entity operator |(Entity e, Component component)
        {
            e.Components.Add(component);
            return e;
        }
    }
}