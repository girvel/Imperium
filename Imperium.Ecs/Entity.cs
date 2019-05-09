using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Imperium.Ecs.Attributes;
using Imperium.Ecs.Managers;
using Province.ToString;

namespace Imperium.Ecs
{
    public class Entity
    {
        public string Name { get; set; }

        [Representative]
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
        }
        
        public void RemoveComponent(Component component)
        {
            Components.Remove(component);
            Ecs.ComponentManager.Unregister(component);
            
            component.Ecs = null;
            component.Parent = null;
        }
        
        public T GetComponent<T>() where T : Component
        {
            return Components.OfType<T>().FirstOrDefault();
        }
        
        public override string ToString() => this.ToRepresentativeString();



        /// <summary>
        /// Adds component to a prototype 
        /// </summary>
        /// <param name="e">Prototype</param>
        /// <param name="component">New component</param>
        /// <returns>Given protype</returns>
        public static Entity operator |(Entity e, Component component)
        {
            e.Components.Add(component);
            component.Parent = e;
            RequirementsAttribute.CheckRequirements(component);
            return e;
        }

        /// <summary>
        /// Checks is the entity a child of the prototype
        /// </summary>
        /// <param name="e">Checking entity</param>
        /// <param name="prototype">Possible prototype of entity</param>
        /// <returns>e.Prototype == prototype</returns>
        public static bool operator <(Entity e, Entity prototype) => e.Prototype == prototype;

        /// <summary>
        /// Checks is the entity a child of the prototype
        /// </summary>
        /// <param name="prototype">Possible prototype of entity</param>
        /// <param name="e">Checking entity</param>
        /// <returns>e.Prototype == prototype</returns>
        public static bool operator >(Entity prototype, Entity e) => e < prototype;

        /// <summary>
        /// Checks does the container contain a prototype of entity
        /// </summary>
        /// <param name="e">Entity</param>
        /// <param name="container">Container of prototypes</param>
        /// <returns>True, if there is a prototype of e in container, else false</returns>
        public static bool operator ^(Entity e, PrototypeContainer container)
            => container
                .Subjects
                .Any(p => e < p);
    }
}