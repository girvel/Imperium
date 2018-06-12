using System;
using System.Threading;

namespace Imperium.Ecs.Managers
{
    public class EcsManager
    {
        public virtual ComponentManager ComponentManager { get; set; }
        
        public EntityManager EntityManager { get; set; }
        
        public SystemManager SystemManager { get; set; }



        public bool Enabled = false;

        public TimeSpan UpdateDelay = TimeSpan.FromSeconds(1.0 / 30);
        
        public void Start()
        {
            Enabled = true;
            while (Enabled)
            {
                SystemManager.Update();
                Thread.Sleep(UpdateDelay);
            }
        }



        public static EcsManager CreateNew()
        {
            var result = new EcsManager
            {
                ComponentManager = new ComponentManager(),
            };
            
            result.SystemManager = new SystemManager {Ecs = result};
            result.EntityManager = new EntityManager {Ecs = result};

            return result;
        }
    }
}