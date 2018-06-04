using Imperium.Ecs.Managers;

namespace Imperium.Ecs
{
    public class Component
    {
        public Entity Owner { get; set; }
        
        public EcsManager Ecs { get; set; }
        
        
        
        public virtual void Start()
        {
        }
    }
}