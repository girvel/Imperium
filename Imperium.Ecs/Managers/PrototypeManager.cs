using System.Linq;

namespace Imperium.Ecs.Managers
{
    public class PrototypeManager : Manager<PrototypeContainer>
    {
        public EcsManager Ecs { get; set; }
        
        public TContainer GetContainer<TContainer>() where TContainer : PrototypeContainer 
            => Subjects.OfType<TContainer>().FirstOrDefault();

        public override void Register(PrototypeContainer subject)
        {
            base.Register(subject);
            
            subject.Initialize(Ecs);
        }
    }
}