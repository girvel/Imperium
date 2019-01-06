﻿namespace Imperium.Ecs.Managers
{
    public class ComponentManager : Manager<Component>
    {
        public virtual Component Create(Component prototype)
        {
            var clone = (Component) prototype.Clone();
            clone.Prototype = prototype;
            Register(clone);
            return clone;
        }

        public override void Unregister(Component subject)
        {
            base.Unregister(subject);

            subject.Destroy();
        }
    }
}