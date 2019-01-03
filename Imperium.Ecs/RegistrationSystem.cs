using System.Collections.Generic;

namespace Imperium.Ecs
{
    public abstract class RegistrationSystem<TSubject> : System
    {
        public List<TSubject> Subjects { get; set; } = new List<TSubject>();
        
        public virtual void Register(TSubject subject)
        {
            Subjects.Add(subject);
        }
    }
}