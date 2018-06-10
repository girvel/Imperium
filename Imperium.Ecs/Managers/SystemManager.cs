using System.Linq;

namespace Imperium.Ecs.Managers
{
    public class SystemManager : Manager<ISystem>
    {
        public void Update()
        {
            foreach (var subject in Subjects)
            {
                subject.Update();
            }
        }

        public virtual T GetSystem<T>()
        {
            return Subjects.OfType<T>().First();
        }
    }
}