using System.Collections.Generic;

namespace Imperium.Ecs.Managers
{
    public class Manager<T>
    {
        public List<T> Subjects { get; set; } = new List<T>();



        public virtual void Register(T subject)
        {
            Subjects.Add(subject);
        }
    }
}