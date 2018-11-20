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
        
        public Entity Original { get; set; }



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
            component.Ecs = null;
            component.Parent = null;
            
            Components.Remove(component);
            Ecs.ComponentManager.Unregister(component);
            
            component.Destroy();
        }
        
        public T GetComponent<T>()
        {
            return Components.OfType<T>().FirstOrDefault();
        }
        
        public override string ToString()
            => $"[Entity \"{Name}\": " +
               $"{{{Components.Aggregate("", (sum, c) => sum + ", " + c.ToString()).Substring(2)}}}]";
    }
}