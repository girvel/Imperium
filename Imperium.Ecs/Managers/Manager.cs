using System.Collections.Generic;
using Province.ToString;

namespace Imperium.Ecs.Managers
{
    public class Manager<T>
    {
        public List<T> Subjects { get; set; } = new List<T>();



        public virtual void Register(T subject)
        {
            Subjects.Add(subject);
        }

        public virtual void Unregister(T subject)
        {
            Subjects.Remove(subject);
        }
        
        public override string ToString() => this.ToRepresentativeString();
    }
}