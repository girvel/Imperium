namespace Imperium.Ecs.Managers
{
    public class ComponentManager : Manager<Component>
    {
        public virtual Component CreateClone(Component original)
        {
            var clone = (Component) original.Clone();
            Register(clone);
            return clone;
        }
    }
}