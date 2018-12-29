using System.Linq;

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

        public virtual T GetSystem<T>()
        {
            return Subjects.OfType<T>().First();
        }
    }
}