using System.CodeDom;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imperium.Ecs
{
    public abstract class ParallelRegistrationSystem<T> : RegistrationSystem<T>
    {
        public abstract void UpdateSubject(T atom);
        
        public sealed override void Update()
        {
            //Parallel.ForEach(Subjects, UpdateSubject);

            foreach (var subject in Subjects)
            {
                UpdateSubject(subject);
            }
        }
    }
}