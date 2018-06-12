using Imperium.Ecs.Managers;

namespace Imperium.Ecs
{
    public class System
    {
        public EcsManager Ecs { get; set; }
        
        public virtual void Update()
        {
            
        }
    }
}