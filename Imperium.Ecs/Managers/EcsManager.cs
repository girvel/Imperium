using System;
using System.Threading;

namespace Imperium.Ecs.Managers
{
    public class EcsManager
    {
        public virtual ComponentManager ComponentManager { get; set; }
        
        public EntityManager EntityManager { get; set; }
        
        public SystemManager SystemManager { get; set; }
        
        public PrototypeManager PrototypeManager { get; set; }



        public bool Enabled = false;

        public TimeSpan UpdateDelay = TimeSpan.FromSeconds(1.0 / 30);

        private long _updateCounter = 0, _updateTotalTicks = 0;
        
        public void Start()
        {
            Enabled = true;
            while (Enabled)
            {
                var now = DateTime.Now;
                
                SystemManager.Update();
                
                _updateCounter++;
                _updateTotalTicks += (DateTime.Now - now).Ticks;
                if (_updateCounter % 100 == 0) 
                    Console.WriteLine($"median update time is {_updateTotalTicks / _updateCounter} ticks");
                
                Thread.Sleep(UpdateDelay);
            }
        }



        public T GetSystem<T>() where T : System => SystemManager.GetSystem<T>();

        public T GetContainer<T>() where T : PrototypeContainer => PrototypeManager.GetContainer<T>();



        public static EcsManager CreateNew()
        {
            var result = new EcsManager
            {
                ComponentManager = new ComponentManager(),
            };
            
            result.SystemManager = new SystemManager {Ecs = result};
            result.EntityManager = new EntityManager {Ecs = result};
            result.PrototypeManager = new PrototypeManager {Ecs = result};

            return result;
        }
    }
}