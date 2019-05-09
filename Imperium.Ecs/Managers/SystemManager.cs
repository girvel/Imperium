using System.Collections.Generic;
using System.Linq;
using Province.ToString;

namespace Imperium.Ecs.Managers
{
    public class SystemManager : Manager<System>
    {
        public EcsManager Ecs { get; set; }
        
        
        
        public void Update()
        {
            foreach (var subject in Subjects)
            {
                subject.Update();
            }
        }

        public override void Register(System subject)
        {
            base.Register(subject);
            subject.Ecs = Ecs;
            subject.Start();
        }

        public virtual T GetSystem<T>() where T : System
        {
            return GetSystems<T>().First();
        }

        public virtual IEnumerable<T> GetSystems<T>() where T : System => Subjects.OfType<T>();



        [Representative]
        private List<System> Systems => Subjects;
    }
}