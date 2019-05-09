using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Imperium.Ecs.Attributes;

namespace Imperium.Ecs
{
    public abstract class RegistrationSystem<TComponent> : System
        where TComponent : RegisteredComponent<TComponent>
    {
        public List<TComponent> Subjects { get; set; } = new List<TComponent>();

        public virtual void Register(TComponent subject)
        {
            Subjects.Add(subject);
        }

        public override void Update()
        {
            base.Update();
            
            GlobalUpdate();
            UpdateSubjects();
        }

        public void UpdateSubjects()
        {
            foreach (var subject in Subjects)
            {
                subject.Update();
            }
        }

        public virtual void GlobalUpdate()
        {
            
        }
    }
}